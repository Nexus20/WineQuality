using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class GrapeSortService : IGrapeSortService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GrapeSortService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GrapeSortDetailsResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.GrapeSorts.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(id));

        var result = _mapper.Map<GrapeSort, GrapeSortDetailsResult>(source);

        return result;
    }

    public async Task<List<GrapeSortResult>> GetAsync(GetGrapeSortsRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.GrapeSorts.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<GrapeSortResult>();

        var result = _mapper.Map<List<GrapeSort>, List<GrapeSortResult>>(source);

        return result;
    }

    public async Task<GrapeSortResult> CreateAsync(CreateGrapeSortRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(GrapeSort)} with such name {request.Name} already exists");
        
        var grapeSortToAdd = _mapper.Map<CreateGrapeSortRequest, GrapeSort>(request);

        await _unitOfWork.GrapeSorts.AddAsync(grapeSortToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(grapeSortToAdd);
        return result;
    }

    public async Task<GrapeSortResult> UpdateAsync(UpdateGrapeSortRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSortToUpdate = await _unitOfWork.GrapeSorts.GetByIdAsync(request.Id, cancellationToken);
        
        if (grapeSortToUpdate == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(GrapeSort)} with such name {request.Name} already exists");

        _mapper.Map(request, grapeSortToUpdate);

        _unitOfWork.GrapeSorts.Update(grapeSortToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(grapeSortToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var grapeSortToDelete = await _unitOfWork.GrapeSorts.GetByIdAsync(id, cancellationToken);
        
        if (grapeSortToDelete == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(id));
        
        _unitOfWork.GrapeSorts.Remove(grapeSortToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AssignGrapeSortToPhaseAsync(AssignGrapeSortToPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSortPhaseWithSameOrderExists = await _unitOfWork.GrapeSortPhases.ExistsAsync(x => x.GrapeSortId == request.GrapeSortId && x.PhaseId == request.PhaseId && x.Order == request.Order, cancellationToken: cancellationToken);

        if (grapeSortPhaseWithSameOrderExists)
            throw new ValidationException($"Grape sort phase with order {request.Order} already exists");
        
        var grapeSort = await _unitOfWork.GrapeSorts.GetByIdAsync(request.GrapeSortId, cancellationToken);
        
        if (grapeSort == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.GrapeSortId));
        
        var processPhase = await _unitOfWork.ProcessPhases.GetByIdAsync(request.PhaseId, cancellationToken);
        
        if (processPhase == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.PhaseId));

        var entityToAdd = _mapper.Map<AssignGrapeSortToPhaseRequest, GrapeSortPhase>(request);
        
        await _unitOfWork.GrapeSortPhases.AddAsync(entityToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GrapeSortPhaseResult>> GetPhasesByGrapeSortIdAsync(string grapeSortId, CancellationToken cancellationToken = default)
    {
        var grapeSortExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Id == grapeSortId, cancellationToken);

        if (!grapeSortExists)
            throw new NotFoundException(nameof(GrapeSort), nameof(grapeSortId));

        var grapeSortPhases =
            await _unitOfWork.GrapeSortPhases.GetAsync(x => x.GrapeSortId == grapeSortId, cancellationToken);

        var result = _mapper.Map<List<GrapeSortPhase>, List<GrapeSortPhaseResult>>(grapeSortPhases);

        return result;
    }

    public async Task SaveGrapeSortPhasesOrderAsync(SaveGrapeSortPhasesOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var grapeSort = await _unitOfWork.GrapeSorts.GetByIdAsync(request.GrapeSortId, cancellationToken);
        
        if (grapeSort == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.GrapeSortId));

        var phasesIds = request.Phases.Select(x => x.PhaseId).ToList();
        var phases = await _unitOfWork.ProcessPhases.GetAsync(x => phasesIds.Contains(x.Id), cancellationToken: cancellationToken);

        if (phases.Count != phasesIds.Count)
            throw new ValidationException("One or more phase ids are invalid");
        
        if(grapeSort.Phases?.Any() == true)
            _unitOfWork.GrapeSortPhases.RemoveRange(grapeSort.Phases);

        var grapeSortPhasesToAdd = request.Phases.Select(x => new GrapeSortPhase()
        {
            GrapeSort = grapeSort,
            GrapeSortId = request.GrapeSortId,
            Order = x.Order,
            PhaseId = x.PhaseId
        });
        
        
        await _unitOfWork.GrapeSortPhases.AddRangeAsync(grapeSortPhasesToAdd, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<GrapeSort, bool>>? CreateFilterPredicate(GetGrapeSortsRequest request)
    {
        Expression<Func<GrapeSort, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}