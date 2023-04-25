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

    public async Task<GrapeSortResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.ProcessPhaseParameters.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhaseParameter), nameof(id));

        var result = _mapper.Map<ProcessPhaseParameter, GrapeSortResult>(source);

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
        
        var processPhaseParameterToAdd = _mapper.Map<CreateGrapeSortRequest, GrapeSort>(request);

        await _unitOfWork.GrapeSorts.AddAsync(processPhaseParameterToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(processPhaseParameterToAdd);
        return result;
    }

    public async Task<GrapeSortResult> UpdateAsync(UpdateGrapeSortRequest request, CancellationToken cancellationToken = default)
    {
        var processPhaseParameterToUpdate = await _unitOfWork.GrapeSorts.GetByIdAsync(request.Id, cancellationToken);
        
        if (processPhaseParameterToUpdate == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(GrapeSort)} with such name {request.Name} already exists");

        _mapper.Map(request, processPhaseParameterToUpdate);

        _unitOfWork.GrapeSorts.Update(processPhaseParameterToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(processPhaseParameterToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var processPhaseParameterToDelete = await _unitOfWork.GrapeSorts.GetByIdAsync(id, cancellationToken);
        
        if (processPhaseParameterToDelete == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(id));
        
        _unitOfWork.GrapeSorts.Delete(processPhaseParameterToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<GrapeSort, bool>>? CreateFilterPredicate(GetGrapeSortsRequest request)
    {
        Expression<Func<GrapeSort, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}