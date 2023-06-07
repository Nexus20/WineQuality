using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Helpers.Expressions;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.WineMaterialBatches;
using WineQuality.Application.Specifications.Abstract;
using WineQuality.Application.Specifications.WineMaterialBatches;
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
    
    public async Task<WineMaterialBatchProcessStartAllowedResult> CheckIfProcessStartAllowedAsync(string id, CancellationToken cancellationToken = default)
    {
        var wineMaterialBatch = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);

        if (wineMaterialBatch == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = new WineMaterialBatchProcessStartAllowedResult();

        if (wineMaterialBatch.Phases.Any(x => x.IsActive))
        {
            throw new ValidationException("Process has been already started");
        }

        if (wineMaterialBatch.Phases.Any(x => x.StartDate == DateTime.MinValue || x.EndDate == DateTime.MinValue))
        {
            result.StartNotAllowedReasons.Add("Process can't be started because phases terms are not set");
        }

        var firstPhase = wineMaterialBatch.Phases.MinBy(x => x.GrapeSortPhase.Order);
        
        var phaseSensorsNotReady = firstPhase.Parameters.Any(p =>
            p.Sensors == null 
            || p.Sensors.Count == 0 
            || p.Sensors.All(x => x.Status != DeviceStatus.Stopped 
                                  && x.Status != DeviceStatus.BoundariesUpdated));

        if (phaseSensorsNotReady)
        {
            result.StartNotAllowedReasons.Add($"Phase \"{firstPhase.GrapeSortPhase.Phase.Name}\" doesn't have ready sensors for all parameters");
        }

        if (!result.StartNotAllowedReasons.Any())
            result.StartAllowed = true;

        return result;
    }
    
    public async Task<WineMaterialBatchProcessStartAllowedResult> CheckIfPhaseStartAllowedAsync(string phaseId, CancellationToken cancellationToken)
    {
        var phase = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(phaseId, cancellationToken);
        
        if (phase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(phaseId));
        
        var result = new WineMaterialBatchProcessStartAllowedResult();
        
        if (phase.IsActive)
        {
            throw new ValidationException("Phase has been already started");
        }

        var phaseOrder = phase.GrapeSortPhase.Order;

        if (phaseOrder != 1)
        {
            var previousActivePhase = await _unitOfWork.WineMaterialBatchGrapeSortPhases.FirstOrDefaultAsync(x => x.WineMaterialBatchId == phase.WineMaterialBatchId && phase.GrapeSortPhase.Order < phaseOrder && phase.IsActive, cancellationToken);

            if (previousActivePhase != null)
            {
                result.StartNotAllowedReasons.Add($"Phase \"{previousActivePhase.GrapeSortPhase.Phase.Name}\" has to be finished first");
            }
        } 
        
        if (phase.StartDate == DateTime.MinValue || phase.EndDate == DateTime.MinValue)
        {
            result.StartNotAllowedReasons.Add("Phase can't be started because terms are not set");
        }
        
        var phaseSensorsNotReady = phase.Parameters.Any(p =>
            p.Sensors == null 
            || p.Sensors.Count == 0 
            || p.Sensors.All(x => x.Status != DeviceStatus.Stopped 
                                  && x.Status != DeviceStatus.BoundariesUpdated));
        
        if (phaseSensorsNotReady)
        {
            result.StartNotAllowedReasons.Add($"Phase \"{phase.GrapeSortPhase.Phase.Name}\" doesn't have ready sensors for all parameters");
        }
        
        if (!result.StartNotAllowedReasons.Any())
            result.StartAllowed = true;
            
        return result;
    }

    public async Task<WineMaterialBatchPhaseParameterChartDataResult> GetPhaseParametersChartAsync(GetWineMaterialBatchPhaseParameterChartDataRequest request,
        CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatchGrapeSortPhaseParameterValues.GetAsync(x => x.PhaseParameterId == request.WineMaterialBatchGrapeSortPhaseParameterId, cancellationToken);
        
        if(source == null || !source.Any())
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhaseParameter), nameof(request.WineMaterialBatchGrapeSortPhaseParameterId));

        var result = new WineMaterialBatchPhaseParameterChartDataResult();
        
        switch(request.ChartType)
        {
            case ParameterChartType.Hour:
                result = AggregateByMinutes(source, 5);
                break;
            case ParameterChartType.Day:
                result = AggregateByHours(source);
                break;
            case ParameterChartType.Week:
                result = AggregateByDays(source, 7, 1);
                break;
            case ParameterChartType.Month:
                result = AggregateByDays(source, 30, 1);
                break;
            case ParameterChartType.Year:
            case ParameterChartType.AllTime:
                result = AggregateByMonths(source);
                break;
        }

        return result;
    }

    private WineMaterialBatchPhaseParameterChartDataResult AggregateByMonths(List<WineMaterialBatchGrapeSortPhaseParameterValue> source)
    {
        var result = new WineMaterialBatchPhaseParameterChartDataResult();

        source.OrderBy(x => x.CreatedAt)
            .GroupBy(p => new
            {
                p.CreatedAt.Year,
                p.CreatedAt.Month
            })
            .ToList()
            .ForEach(g =>
            {
                var label = new DateTime(g.Key.Year, g.Key.Month, 1);
                var value = g.Average(p => p.Value);
                result.Labels.Add(label);
                result.Values.Add(value);
            });

        return result;
    }

    private WineMaterialBatchPhaseParameterChartDataResult AggregateByDays(List<WineMaterialBatchGrapeSortPhaseParameterValue> source, int daysCount, int aggregateByDaysCount)
    {
        var result = new WineMaterialBatchPhaseParameterChartDataResult();

        source.Where(x => DateTime.UtcNow - x.CreatedAt < TimeSpan.FromDays(daysCount))
            .Where(x => x.CreatedAt.Day % aggregateByDaysCount == 0)
            .OrderBy(x => x.CreatedAt)
            .GroupBy(p => new
            {
                p.CreatedAt.Year,
                p.CreatedAt.Month,
                p.CreatedAt.Day
            })
            .ToList()
            .ForEach(g =>
            {
                var label = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day);
                var value = g.Average(p => p.Value);
                result.Labels.Add(label);
                result.Values.Add(value);
            });

        return result;
    }
    
    private WineMaterialBatchPhaseParameterChartDataResult AggregateByHours(List<WineMaterialBatchGrapeSortPhaseParameterValue> source)
    {
        var result = new WineMaterialBatchPhaseParameterChartDataResult();

        source.Where(x => DateTime.UtcNow - x.CreatedAt < TimeSpan.FromDays(1))
            .OrderBy(x => x.CreatedAt)
            .GroupBy(p => new
            {
                p.CreatedAt.Year,
                p.CreatedAt.Month,
                p.CreatedAt.Day,
                p.CreatedAt.Hour
            })
            .ToList()
            .ForEach(g =>
            {
                var label = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, 0, 0);
                var value = g.Average(p => p.Value);
                result.Labels.Add(label);
                result.Values.Add(value);
            });

        return result;
    }

    private WineMaterialBatchPhaseParameterChartDataResult AggregateByMinutes(List<WineMaterialBatchGrapeSortPhaseParameterValue> source, int i)
    {
        var result = new WineMaterialBatchPhaseParameterChartDataResult();

        source.Where(x => DateTime.UtcNow - x.CreatedAt < TimeSpan.FromHours(1))
            .Where(x => x.CreatedAt.Minute % 5 == 0)
            .OrderBy(x => x.CreatedAt)
            .GroupBy(p => new
            {
                p.CreatedAt.Year,
                p.CreatedAt.Month,
                p.CreatedAt.Day,
                p.CreatedAt.Hour,
                p.CreatedAt.Minute
            })
            .ToList()
            .ForEach(g =>
            {
                var label = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, g.Key.Minute, 0);
                var value = g.Average(p => p.Value);
                result.Labels.Add(label);
                result.Values.Add(value);
            });

        return result;
    }

    public async Task<WineMaterialBatchGrapeSortPhaseDetailsResult> GetPhaseDetailsByIdAsync(string phaseId, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(phaseId, new WineMaterialBatchGrapeSortPhaseIncludeDetailsSpecification(), cancellationToken);
        
        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(phaseId));
        
        var result = _mapper.Map<WineMaterialBatchGrapeSortPhase, WineMaterialBatchGrapeSortPhaseDetailsResult>(source);
        
        return result;
    }

    public async Task<WineMaterialBatchResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchResult>(source);
        
        result.Phases = result.Phases.OrderBy(x => x.Phase.Order).ToList();

        return result;
    }

    public async Task<WineMaterialBatchDetailsResult> GetDetailsByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, new WineMaterialBatchIncludeDetailsSpecification(), cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchDetailsResult>(source);

        result.Phases = result.Phases.OrderBy(x => x.Phase.Order).ToList();
        
        return result;
    }

    public async Task UpdatePhasesTermsAsync(UpdateWineMaterialBatchPhasesTermsRequest request,
        CancellationToken cancellationToken = default)
    {
        var wineMaterialBatchPhasesIds = request.Terms.Select(x => x.Id).ToList();

        var wineMaterialBatchPhases =
            await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetAsync(x => wineMaterialBatchPhasesIds.Contains(x.Id),
                cancellationToken);

        if (wineMaterialBatchPhasesIds.Count != wineMaterialBatchPhases.Count)
            throw new ValidationException("One or more wine material batch phases ids are invalid");
        
        foreach (var wineMaterialBatchGrapeSortPhase in wineMaterialBatchPhases)
        {
            var relatedRequestPart = request.Terms.Single(x => x.Id == wineMaterialBatchGrapeSortPhase.Id);
            wineMaterialBatchGrapeSortPhase.StartDate = relatedRequestPart.StartDate;
            wineMaterialBatchGrapeSortPhase.EndDate = relatedRequestPart.EndDate;
            
            _unitOfWork.WineMaterialBatchGrapeSortPhases.Update(wineMaterialBatchGrapeSortPhase);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<WineMaterialBatchResult>> GetAsync(GetWineMaterialBatchesRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var filterSpecification = predicate == null
            ? FilterSpecification<WineMaterialBatch>.Default
            : new FilterSpecification<WineMaterialBatch>(predicate);
        
        var source = await _unitOfWork.WineMaterialBatches.GetBySpecificationAsync(filterSpecification, cancellationToken: cancellationToken);

        if (source == null || !source.Any())
            return new List<WineMaterialBatchResult>();

        var result = _mapper.Map<List<WineMaterialBatch>, List<WineMaterialBatchResult>>(source);

        return result;
    }

    public async Task<WineMaterialBatchResult> CreateAsync(CreateWineMaterialBatchRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSort = await _unitOfWork.GrapeSorts.GetByIdAsync(request.GrapeSortId, cancellationToken);

        if (grapeSort == null)
            throw new ValidationException($"Grape sort id {request.GrapeSortId} is invalid");

        var duplicateExists = await _unitOfWork.WineMaterialBatches.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(WineMaterialBatch)} with such name {request.Name} already exists");

        var wineMaterialBatchToAdd = _mapper.Map<CreateWineMaterialBatchRequest, WineMaterialBatch>(request);
        wineMaterialBatchToAdd.Phases = grapeSort.Phases.Select(x =>
            {
                var wineMaterialBatchGrapeSortPhase = new WineMaterialBatchGrapeSortPhase
                {
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MinValue,
                    GrapeSortPhaseId = x.Id,
                    GrapeSortPhase = x,
                    WineMaterialBatchId = wineMaterialBatchToAdd.Id,
                    WineMaterialBatch = wineMaterialBatchToAdd,
                    IsActive = false,
                };

                wineMaterialBatchGrapeSortPhase.Parameters = x.Phase.Parameters.Select(pp =>
                        new WineMaterialBatchGrapeSortPhaseParameter
                        {
                            WineMaterialBatchGrapeSortPhase = wineMaterialBatchGrapeSortPhase,
                            WineMaterialBatchGrapeSortPhaseId = wineMaterialBatchGrapeSortPhase.Id,
                            PhaseParameter = pp,
                            PhaseParameterId = pp.Id,
                        })
                    .ToList();

                return wineMaterialBatchGrapeSortPhase;
            })
            .ToList();

        await _unitOfWork.WineMaterialBatches.AddAsync(wineMaterialBatchToAdd, cancellationToken);
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
        
        if(wineMaterialBatchToDelete.Phases.Any(x => x.IsActive))
            throw new ValidationException("Cannot delete wine material batch that has active phase");
        
        // TODO: Add cascade delete
        foreach (var wineMaterialBatchGrapeSortPhase in wineMaterialBatchToDelete.Phases)
        {
            if (wineMaterialBatchGrapeSortPhase.Parameters?.Any() == true)
            {
                foreach (var wineMaterialBatchGrapeSortPhaseParameter in wineMaterialBatchGrapeSortPhase.Parameters)
                {
                    if (wineMaterialBatchGrapeSortPhaseParameter.Values?.Any() == true)
                    {
                        _unitOfWork.WineMaterialBatchGrapeSortPhaseParameterValues.RemoveRange(wineMaterialBatchGrapeSortPhaseParameter.Values);
                    }

                    if (wineMaterialBatchGrapeSortPhaseParameter.Sensors?.Any() == true)
                    {
                        foreach (var processPhaseParameterSensor in wineMaterialBatchGrapeSortPhaseParameter.Sensors)
                        {
                            processPhaseParameterSensor.WineMaterialBatchGrapeSortPhaseParameter = null;
                            processPhaseParameterSensor.WineMaterialBatchGrapeSortPhaseParameterId = null;
                            _unitOfWork.ProcessPhaseParameterSensors.Update(processPhaseParameterSensor);
                        }
                    }
                }
                
                _unitOfWork.WineMaterialBatchGrapeSortPhaseParameters.RemoveRange(wineMaterialBatchGrapeSortPhase.Parameters);
            }
        }
        
        _unitOfWork.WineMaterialBatchGrapeSortPhases.RemoveRange(wineMaterialBatchToDelete.Phases);
        _unitOfWork.WineMaterialBatches.Remove(wineMaterialBatchToDelete);
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

        var phase = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(request.WineMaterialBatchGrapeSortPhaseId, new WineMaterialBatchGrapeSortPhaseIncludeDetailsSpecification(), cancellationToken);
        // var phase = wineMaterialBatchGrapeSortPhases.SingleOrDefault(x => x.Id == request.WineMaterialBatchGrapeSortPhaseId);

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

        if (!string.IsNullOrWhiteSpace(request.GrapeSortId))
        {
            Expression<Func<WineMaterialBatch, bool>> grapeSortPredicate = x => x.GrapeSortId == request.GrapeSortId;
            predicate = ExpressionsHelper.And(predicate, grapeSortPredicate);
        }
        
        return predicate;
    }
}