using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Application.Models.Results.WineMaterialBatches;
using WineQuality.Domain.Entities;
using WineQuality.Domain.Enums;

namespace WineQuality.Application.Services;

public class WineMaterialBatchService : IWineMaterialBatchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIoTDeviceService _ioTDeviceService;

    public WineMaterialBatchService(IUnitOfWork unitOfWork, IMapper mapper, IIoTDeviceService ioTDeviceService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ioTDeviceService = ioTDeviceService;
    }

    public async Task<WineMaterialBatchResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchResult>(source);

        return result;
    }

    public async Task<WineMaterialBatchDetailsResult> GetDetailsByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = new WineMaterialBatchDetailsResult()
        {
            Id = source.Id,
            Name = source.Name,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt,
            HarvestDate = source.HarvestDate,
            HarvestLocation = source.HarvestLocation,
            GrapeSort = _mapper.Map<GrapeSort, GrapeSortResult>(source.GrapeSort),
        };

        var activePhase = source.Phases.FirstOrDefault(p => p.IsActive);

        if (activePhase != null)
        {
            result.ActivePhase = _mapper.Map<WineMaterialBatchGrapeSortPhase, WineMaterialBatchProcessPhaseResult>(activePhase);
            result.ParametersReadings = activePhase.Parameters.Select(p =>
                {
                    var readingsResult = new WineMaterialBatchProcessPhaseReadingsResult
                    {
                        Value = p.Values.MaxBy(v => v.CreatedAt).Value,
                        ParameterName = p.PhaseParameter.Parameter.Name,
                        ParameterId = p.PhaseParameter.Parameter.Id
                    };

                    return readingsResult;
                })
                .ToList();
        }

        return result;
    }

    public async Task<List<WineMaterialBatchResult>> GetAsync(GetWineMaterialBatchesRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.WineMaterialBatches.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<WineMaterialBatchResult>();

        var result = _mapper.Map<List<WineMaterialBatch>, List<WineMaterialBatchResult>>(source);

        return result;
    }

    public async Task<WineMaterialBatchResult> CreateAsync(CreateWineMaterialBatchRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.WineMaterialBatches.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(WineMaterialBatch)} with such name {request.Name} already exists");
        
        var wineMaterialBatchToAdd = _mapper.Map<CreateWineMaterialBatchRequest, WineMaterialBatch>(request);

        await _unitOfWork.WineMaterialBatches.AddAsync(wineMaterialBatchToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchResult>(wineMaterialBatchToAdd);
        return result;
    }

    public async Task<WineMaterialBatchResult> UpdateAsync(UpdateWineMaterialBatchRequest request, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchToUpdate = await _unitOfWork.WineMaterialBatches.GetByIdAsync(request.Id, cancellationToken);
        
        if (wineMaterialBatchToUpdate == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.WineMaterialBatches.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(WineMaterialBatch)} with such name {request.Name} already exists");

        _mapper.Map(request, wineMaterialBatchToUpdate);

        _unitOfWork.WineMaterialBatches.Update(wineMaterialBatchToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchResult>(wineMaterialBatchToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchToDelete = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);
        
        if (wineMaterialBatchToDelete == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));
        
        _unitOfWork.WineMaterialBatches.Delete(wineMaterialBatchToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AssignWineMaterialBatchToPhasesAsync(AssignWineMaterialBatchToPhasesRequest request,
        CancellationToken cancellationToken = default)
    {
        var wineMaterialBatch = await _unitOfWork.WineMaterialBatches.GetByIdAsync(request.WineMaterialBatchId, cancellationToken);
        
        if (wineMaterialBatch == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(request.WineMaterialBatchId));

        var grapeSortPhasesIds = request.Phases.Select(x => x.GrapeSortPhaseId);
        
        var grapeSortPhases = await _unitOfWork.GrapeSortPhases.GetAsync(x => grapeSortPhasesIds.Contains(x.Id), cancellationToken);
        
        if (grapeSortPhases.Count != grapeSortPhasesIds.Count())
            throw new ValidationException("At least one grape sort phase id is invalid");

        var entitiesToAdd = request.Phases.Select(x => new WineMaterialBatchGrapeSortPhase
        {
            GrapeSortPhaseId = x.GrapeSortPhaseId,
            WineMaterialBatchId = request.WineMaterialBatchId,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
        }).ToList();

        foreach (var entityToAdd in entitiesToAdd)
        {
            var grapeSortPhase =
                await _unitOfWork.GrapeSortPhases.GetByIdAsync(entityToAdd.GrapeSortPhaseId, cancellationToken);

            var parametersToAdd = grapeSortPhase!.Phase.Parameters
                .Select(x => new WineMaterialBatchGrapeSortPhaseParameter 
                {
                    WineMaterialBatchGrapeSortPhaseId = entityToAdd.Id,
                    WineMaterialBatchGrapeSortPhase = entityToAdd,
                    PhaseParameterId = x.Id,
                    PhaseParameter = x
                })
                .ToList();

            entityToAdd.Parameters = parametersToAdd;
        }

        await _unitOfWork.WineMaterialBatchGrapeSortPhases.AddRangeAsync(entitiesToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RunWineProcessingForPhaseAsync(ChangeWineProcessingRequestRunningState request, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchGrapeSortPhases = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetAsync(
            x => x.WineMaterialBatchId == request.WineMaterialBatchId, cancellationToken);

        if (!wineMaterialBatchGrapeSortPhases.Any())
            throw new ValidationException(
                $"There are no phases set for wine material batch with id {request.WineMaterialBatchId}");

        var phase = wineMaterialBatchGrapeSortPhases.SingleOrDefault(x => x.Id == request.WineMaterialBatchGrapeSortPhaseId);

        if (phase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase),
                nameof(request.WineMaterialBatchGrapeSortPhaseId));

        if (phase.GrapeSortPhase.Phase.Parameters.Count != phase.GrapeSortPhase.GrapeSortProcessPhaseParameterStandards.Count)
        {
            throw new ValidationException("Not all parameters of this phase have standards");
        }

        if (phase.Parameters.Any(x =>
                x.Sensors == null || !x.Sensors.Any(s => s.Status is DeviceStatus.BoundariesUpdated or DeviceStatus.Stopped)))
        {
            throw new ValidationException("Sensors not configured for all parameters");
        }

        var sensors = phase.Parameters.SelectMany(x => x.Sensors)
            .Where(s => s.Status is DeviceStatus.BoundariesUpdated or DeviceStatus.Stopped)
            .ToList();

        foreach (var sensor in sensors)
        {
            await _ioTDeviceService.RunSensorAsync(sensor, cancellationToken: cancellationToken);
        }

        phase.IsActive = true;
        
        _unitOfWork.WineMaterialBatchGrapeSortPhases.Update(phase);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task StopWineProcessingForPhaseAsync(ChangeWineProcessingRequestRunningState request,
        CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchGrapeSortPhases = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetAsync(
            x => x.WineMaterialBatchId == request.WineMaterialBatchId, cancellationToken);

        if (!wineMaterialBatchGrapeSortPhases.Any())
            throw new ValidationException(
                $"There are no phases set for wine material batch with id {request.WineMaterialBatchId}");

        var phase = wineMaterialBatchGrapeSortPhases.SingleOrDefault(x => x.Id == request.WineMaterialBatchGrapeSortPhaseId);

        if (phase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase),
                nameof(request.WineMaterialBatchGrapeSortPhaseId));
        
        var sensors = phase.Parameters.SelectMany(x => x.Sensors)
            .Where(s => s.Status is DeviceStatus.Working)
            .ToList();

        foreach (var sensor in sensors)
        {
            await _ioTDeviceService.StopSensorAsync(sensor, cancellationToken: cancellationToken);
        }

        phase.IsActive = false;
        
        _unitOfWork.WineMaterialBatchGrapeSortPhases.Update(phase);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<WineMaterialBatch, bool>>? CreateFilterPredicate(GetWineMaterialBatchesRequest request)
    {
        Expression<Func<WineMaterialBatch, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}