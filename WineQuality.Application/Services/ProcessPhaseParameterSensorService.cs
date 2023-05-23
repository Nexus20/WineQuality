using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Helpers.Expressions;
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

    public async Task DeleteSensorAsync(string id, CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(id, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(id));

        if (sensor.Status == DeviceStatus.Working)
            throw new ValidationException("Unable to delete working sensor");

        await _ioTDeviceService.RemoveDeviceAsync(id, cancellationToken);
        _unitOfWork.ProcessPhaseParameterSensors.Remove(sensor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProcessPhaseParameterSensorResult> CreateSensorAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default)
    {
        var phaseParameter = await _unitOfWork.ProcessPhaseParameters.FirstOrDefaultAsync(x => x.ParameterId == request.ParameterId && x.PhaseId == request.PhaseId,
                cancellationToken);

        if (phaseParameter == null)
            throw new ValidationException($"ProcessPhaseParameter with parameter id #{request.ParameterId} and phase id #{request.PhaseId} does not exist");

        var phaseName = phaseParameter.Phase.Name.ToLower();
        var parameterName = string.Join('-', phaseParameter.Parameter.Name.ToLower().Split(' ', StringSplitOptions.TrimEntries));

        var deviceId = $"{phaseName}-{parameterName}-";

        var phaseParameterSensors =
            await _unitOfWork.ProcessPhaseParameterSensors.GetAsync(x => x.PhaseParameterId == phaseParameter.Id, cancellationToken: cancellationToken);

        if (!phaseParameterSensors.Any())
        {
            deviceId += "1";
        }
        else
        {
            var lastExistingDeviceNumber = phaseParameterSensors.Select(x =>
                {
                    var idParts = x.Id.Split('-');
                    return int.Parse(idParts[^1]);
                })
                .Max();

            lastExistingDeviceNumber++;
            deviceId += $"{lastExistingDeviceNumber}";
        }

        var deviceKey = await _ioTDeviceService.AddDeviceAsync(deviceId, cancellationToken);

        var sensorEntityToAdd = new ProcessPhaseParameterSensor()
        {
            Id = deviceId,
            DeviceKey = deviceKey,
            PhaseParameterId = phaseParameter.Id
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

    public async Task AssignDeviceToWineMaterialBatchAsync(AssignDevicesToWineMaterialBatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var sensors =
            await _unitOfWork.ProcessPhaseParameterSensors.GetAsync(x => request.SensorsIds.Contains(x.Id),
                cancellationToken);
        
        if(sensors.Count != request.SensorsIds.Length)
            throw new ValidationException("Some of the sensors does not exist");
        
        var wineMaterialBatchGrapeSortPhase = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(request.WineMaterialBatchGrapeSortPhaseId, cancellationToken);

        if (wineMaterialBatchGrapeSortPhase == null){
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(request.WineMaterialBatchGrapeSortPhaseId));
        }
        
        foreach (var sensor in sensors)
        {
            var wineMaterialBatchGrapeSortPhaseParameter = wineMaterialBatchGrapeSortPhase.Parameters.Single(x => x.PhaseParameterId == sensor.PhaseParameterId);
            sensor.WineMaterialBatchGrapeSortPhaseParameter = wineMaterialBatchGrapeSortPhaseParameter;
            sensor.WineMaterialBatchGrapeSortPhaseParameterId = wineMaterialBatchGrapeSortPhaseParameter.Id;
            
            var standard = await _unitOfWork.GrapeSortProcessPhaseParameterStandards.FirstOrDefaultAsync(x =>
                x.PhaseParameterId == sensor.PhaseParameterId && x.GrapeSortPhaseId == wineMaterialBatchGrapeSortPhase.GrapeSortPhaseId, cancellationToken);

            if (standard != null)
            {
                await _ioTDeviceService.SendStandardsUpdateMessageAsync(sensor, standard, cancellationToken);
            }
        
            _unitOfWork.ProcessPhaseParameterSensors.Update(sensor);
        }
        
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

        if (!string.IsNullOrWhiteSpace(request.PhaseId))
        {
            Expression<Func<ProcessPhaseParameterSensor, bool>> phasePredicate = x => x.PhaseParameter.PhaseId == request.PhaseId;
            predicate = ExpressionsHelper.And(predicate, phasePredicate);
        }
        
        return predicate;
    }
}