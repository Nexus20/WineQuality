using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseParameters;
using WineQuality.Application.Models.Results.ProcessParameters;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class ProcessParameterService : IProcessParameterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProcessParameterService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProcessParameterResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.ProcessPhaseParameters.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhaseParameter), nameof(id));

        var result = _mapper.Map<ProcessPhaseParameter, ProcessParameterResult>(source);

        return result;
    }

    public async Task<List<ProcessParameterResult>> GetAsync(GetProcessPhaseParametersRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.ProcessParameters.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<ProcessParameterResult>();

        var result = _mapper.Map<List<ProcessParameter>, List<ProcessParameterResult>>(source);

        return result;
    }

    public async Task<ProcessParameterResult> CreateAsync(CreateProcessParameterRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.ProcessParameters.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessParameter)} with such name {request.Name} already exists");
        
        var processPhaseParameterToAdd = _mapper.Map<CreateProcessParameterRequest, ProcessParameter>(request);

        await _unitOfWork.ProcessParameters.AddAsync(processPhaseParameterToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessParameter, ProcessParameterResult>(processPhaseParameterToAdd);
        return result;
    }

    public async Task<ProcessParameterResult> UpdateAsync(UpdateProcessParameterRequest request, CancellationToken cancellationToken = default)
    {
        var processPhaseParameterToUpdate = await _unitOfWork.ProcessParameters.GetByIdAsync(request.Id, cancellationToken);
        
        if (processPhaseParameterToUpdate == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.ProcessParameters.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessParameter)} with such name {request.Name} already exists");

        _mapper.Map(request, processPhaseParameterToUpdate);

        _unitOfWork.ProcessParameters.Update(processPhaseParameterToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessParameter, ProcessParameterResult>(processPhaseParameterToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var processPhaseParameterToDelete = await _unitOfWork.ProcessParameters.GetByIdAsync(id, cancellationToken);
        
        if (processPhaseParameterToDelete == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(id));
        
        _unitOfWork.ProcessParameters.Delete(processPhaseParameterToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<ProcessParameter, bool>>? CreateFilterPredicate(GetProcessPhaseParametersRequest request)
    {
        Expression<Func<ProcessParameter, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}