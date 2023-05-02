using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Application.Models.Results.ProcessPhaseParameterSensors;
using WineQuality.Domain.Entities;
using WineQuality.Domain.Enums;

namespace WineQuality.Application.Services;

public class ProcessPhaseParameterSensorService : IProcessPhaseParameterSensorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIoTDeviceService _ioTDeviceService;
    private readonly IMapper _mapper;

    public ProcessPhaseParameterSensorService(IUnitOfWork unitOfWork, IIoTDeviceService ioTDeviceService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _ioTDeviceService = ioTDeviceService;
        _mapper = mapper;
    }

    public async Task<ProcessPhaseParameterSensorResult> CreateSensorAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default)
    {
        var phaseParameterExists =
            await _unitOfWork.ProcessPhaseParameters.ExistsAsync(x => x.Id == request.PhaseParameterId,
                cancellationToken);

        if (!phaseParameterExists)
            throw new NotFoundException(nameof(ProcessPhaseParameter), nameof(request.PhaseParameterId));
        
        var duplicateExists = await _unitOfWork.ProcessPhaseParameterSensors.ExistsAsync(x => x.Id == request.DeviceId, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"Sensor with such id {request.DeviceId} already exists");

        var deviceKey = await _ioTDeviceService.AddDeviceAsync(request, cancellationToken);

        var sensorEntityToAdd = new ProcessPhaseParameterSensor()
        {
            Id = request.DeviceId,
            DeviceKey = deviceKey,
            PhaseParameterId = request.PhaseParameterId
        };

        await _unitOfWork.ProcessPhaseParameterSensors.AddAsync(sensorEntityToAdd, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var source =
            await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(sensorEntityToAdd.Id, cancellationToken);
        
        var result = _mapper.Map<ProcessPhaseParameterSensor, ProcessPhaseParameterSensorResult>(source!);

        return result;
    }

    public async Task<List<ProcessPhaseParameterSensorResult>> GetAsync(GetProcessPhaseParameterSensorsRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.ProcessPhaseParameterSensors.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<ProcessPhaseParameterSensorResult>();

        var result = _mapper.Map<List<ProcessPhaseParameterSensor>, List<ProcessPhaseParameterSensorResult>>(source);

        return result;
    }

    public async Task<ProcessPhaseParameterSensorResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(id));

        var result = _mapper.Map<ProcessPhaseParameterSensor, ProcessPhaseParameterSensorResult>(source);

        return result;
    }

    public async Task AssignDeviceToWineMaterialBatchAsync(AssignDeviceToWineMaterialBatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(request.DeviceId, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(request.DeviceId));

        var wineMaterialBatchGrapeSortPhaseParameter =
            await _unitOfWork.WineMaterialBatchGrapeSortPhaseParameters.GetByIdAsync(
                request.WineMaterialBatchGrapeSortPhaseParameterId, cancellationToken);
        
        if(wineMaterialBatchGrapeSortPhaseParameter == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhaseParameter), nameof(request.WineMaterialBatchGrapeSortPhaseParameterId));

        sensor.WineMaterialBatchGrapeSortPhaseParameter = wineMaterialBatchGrapeSortPhaseParameter;
        sensor.WineMaterialBatchGrapeSortPhaseParameterId = request.WineMaterialBatchGrapeSortPhaseParameterId;
        
        var grapeSortPhaseId = sensor.WineMaterialBatchGrapeSortPhaseParameter
            .WineMaterialBatchGrapeSortPhase.GrapeSortPhaseId;

        var standard = await _unitOfWork.GrapeSortProcessPhaseParameterStandards.FirstOrDefaultAsync(x =>
            x.PhaseParameterId == sensor.PhaseParameterId && x.GrapeSortPhaseId == grapeSortPhaseId, cancellationToken);

        if (standard != null)
        {
            await _ioTDeviceService.SendStandardsUpdateMessageAsync(sensor, standard, cancellationToken);
        }
        
        _unitOfWork.ProcessPhaseParameterSensors.Update(sensor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RunSensorAsync(string sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(sensorId, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(sensorId));
        
        await _ioTDeviceService.RunSensorAsync(sensor, cancellationToken);
    }

    public async Task StopSensorAsync(string sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(sensorId, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(sensorId));
        
        await _ioTDeviceService.StopSensorAsync(sensor, cancellationToken);
    }

    public async Task RunAllPhaseSensorsAsync(string wineMaterialBatchGrapeSortPhaseId, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchGrapeSortPhase =
            await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(wineMaterialBatchGrapeSortPhaseId,
                cancellationToken);

        if (wineMaterialBatchGrapeSortPhase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(wineMaterialBatchGrapeSortPhaseId));

        if (!wineMaterialBatchGrapeSortPhase.Parameters.Any())
            throw new ValidationException("No parameters have been set for this WineMaterialBatchGrapeSortPhase");

        var sensors = wineMaterialBatchGrapeSortPhase.Parameters.SelectMany(x => x.Sensors)
            .Where(x => x.Status is DeviceStatus.BoundariesUpdated or DeviceStatus.Stopped)
            .ToList();

        foreach (var sensor in sensors)
        {
            await _ioTDeviceService.RunSensorAsync(sensor, cancellationToken);
        }
    }

    public async Task StopAllPhaseSensorsAsync(string wineMaterialBatchGrapeSortPhaseId, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchGrapeSortPhase =
            await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(wineMaterialBatchGrapeSortPhaseId,
                cancellationToken);

        if (wineMaterialBatchGrapeSortPhase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(wineMaterialBatchGrapeSortPhaseId));

        if (!wineMaterialBatchGrapeSortPhase.Parameters.Any())
            throw new ValidationException("No parameters have been set for this WineMaterialBatchGrapeSortPhase");

        var sensors = wineMaterialBatchGrapeSortPhase.Parameters.SelectMany(x => x.Sensors)
            .Where(x => x.Status is DeviceStatus.Working)
            .ToList();

        foreach (var sensor in sensors)
        {
            await _ioTDeviceService.StopSensorAsync(sensor, cancellationToken);
        }
    }

    private Expression<Func<ProcessPhaseParameterSensor, bool>>? CreateFilterPredicate(GetProcessPhaseParameterSensorsRequest request)
    {
        Expression<Func<ProcessPhaseParameterSensor, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}