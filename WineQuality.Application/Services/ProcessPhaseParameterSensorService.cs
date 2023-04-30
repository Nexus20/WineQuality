using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Application.Models.Results.ProcessPhaseParameterSensors;
using WineQuality.Domain.Entities;

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
    
    private Expression<Func<ProcessPhaseParameterSensor, bool>>? CreateFilterPredicate(GetProcessPhaseParameterSensorsRequest request)
    {
        Expression<Func<ProcessPhaseParameterSensor, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}