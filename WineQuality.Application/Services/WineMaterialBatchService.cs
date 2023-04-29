using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.WineMaterialBatches;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class WineMaterialBatchService : IWineMaterialBatchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WineMaterialBatchService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<WineMaterialBatchResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.WineMaterialBatches.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(id));

        var result = _mapper.Map<WineMaterialBatch, WineMaterialBatchResult>(source);

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

    public async Task AssignWineMaterialBatchToPhaseAsync(AssignWineMaterialBatchToPhaseRequest request,
        CancellationToken cancellationToken = default)
    {
        var wineMaterialBatch = await _unitOfWork.WineMaterialBatches.GetByIdAsync(request.WineMaterialBatchId, cancellationToken);
        
        if (wineMaterialBatch == null)
            throw new NotFoundException(nameof(WineMaterialBatch), nameof(request.WineMaterialBatchId));
        
        var processPhaseType = await _unitOfWork.ProcessPhaseTypes.GetByIdAsync(request.PhaseTypeId, cancellationToken);
        
        if (processPhaseType == null)
            throw new NotFoundException(nameof(ProcessPhaseType), nameof(request.PhaseTypeId));

        var entityToAdd = _mapper.Map<AssignWineMaterialBatchToPhaseRequest, WineMaterialBatchProcessPhase>(request);
        
        await _unitOfWork.WineMaterialBatchProcessPhases.AddAsync(entityToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<WineMaterialBatch, bool>>? CreateFilterPredicate(GetWineMaterialBatchesRequest request)
    {
        Expression<Func<WineMaterialBatch, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}