using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class ProcessPhaseTypeService : IProcessPhaseTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProcessPhaseTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProcessPhaseTypeResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.ProcessPhaseTypes.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhaseType), nameof(id));

        var result = _mapper.Map<ProcessPhaseType, ProcessPhaseTypeResult>(source);

        return result;
    }

    public async Task<List<ProcessPhaseTypeResult>> GetAsync(GetProcessPhaseTypesRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.ProcessPhaseTypes.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<ProcessPhaseTypeResult>();

        var result = _mapper.Map<List<ProcessPhaseType>, List<ProcessPhaseTypeResult>>(source);

        return result;
    }

    public async Task<ProcessPhaseTypeResult> CreateAsync(CreateProcessPhaseTypeRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.ProcessPhaseTypes.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessPhaseType)} with such name {request.Name} already exists");
        
        var processPhaseTypeToAdd = _mapper.Map<CreateProcessPhaseTypeRequest, ProcessPhaseType>(request);

        await _unitOfWork.ProcessPhaseTypes.AddAsync(processPhaseTypeToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessPhaseType, ProcessPhaseTypeResult>(processPhaseTypeToAdd);
        return result;
    }

    public async Task<ProcessPhaseTypeResult> UpdateAsync(UpdateProcessPhaseTypeRequest request, CancellationToken cancellationToken = default)
    {
        var processPhaseTypeToUpdate = await _unitOfWork.ProcessPhaseTypes.GetByIdAsync(request.Id, cancellationToken);
        
        if (processPhaseTypeToUpdate == null)
            throw new NotFoundException(nameof(ProcessPhaseType), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.ProcessPhaseTypes.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessPhaseType)} with such name {request.Name} already exists");

        _mapper.Map(request, processPhaseTypeToUpdate);

        _unitOfWork.ProcessPhaseTypes.Update(processPhaseTypeToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessPhaseType, ProcessPhaseTypeResult>(processPhaseTypeToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var processPhaseTypeToDelete = await _unitOfWork.ProcessPhaseTypes.GetByIdAsync(id, cancellationToken);
        
        if (processPhaseTypeToDelete == null)
            throw new NotFoundException(nameof(ProcessPhaseType), nameof(id));
        
        _unitOfWork.ProcessPhaseTypes.Delete(processPhaseTypeToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<ProcessPhaseType, bool>>? CreateFilterPredicate(GetProcessPhaseTypesRequest request)
    {
        Expression<Func<ProcessPhaseType, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}