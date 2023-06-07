using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhases;
using WineQuality.Application.Specifications.Abstract;
using WineQuality.Application.Specifications.ProcessPhases;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class ProcessPhaseService : IProcessPhaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProcessPhaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProcessPhaseResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.ProcessPhases.GetByIdAsync(id, new ProcessPhaseIncludeParametersSpecification(), cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(id));

        var result = _mapper.Map<ProcessPhase, ProcessPhaseDetailsResult>(source);

        return result;
    }

    public async Task<List<ProcessPhaseResult>> GetAsync(GetProcessPhasesRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.ProcessPhases.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<ProcessPhaseResult>();

        var result = _mapper.Map<List<ProcessPhase>, List<ProcessPhaseResult>>(source);
        
        return result;
    }

    public async Task<ProcessPhaseResult> CreateAsync(CreateProcessPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.ProcessPhases.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessPhase)} with such name {request.Name} already exists");
        
        var processPhaseToAdd = _mapper.Map<CreateProcessPhaseRequest, ProcessPhase>(request);

        await _unitOfWork.ProcessPhases.AddAsync(processPhaseToAdd, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessPhase, ProcessPhaseResult>(processPhaseToAdd);
        return result;
    }

    public async Task<ProcessPhaseResult> UpdateAsync(UpdateProcessPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var processPhaseToUpdate = await _unitOfWork.ProcessPhases.GetByIdAsync(request.Id, cancellationToken);
        
        if (processPhaseToUpdate == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.ProcessPhases.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessPhase)} with such name {request.Name} already exists");

        _mapper.Map(request, processPhaseToUpdate);

        _unitOfWork.ProcessPhases.Update(processPhaseToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessPhase, ProcessPhaseResult>(processPhaseToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var processPhaseToDelete = await _unitOfWork.ProcessPhases.GetByIdAsync(id, cancellationToken);
        
        if (processPhaseToDelete == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(id));
        
        _unitOfWork.ProcessPhases.Remove(processPhaseToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddParametersAsync(AddParametersToPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var processPhase = await _unitOfWork.ProcessPhases.GetByIdAsync(request.ProcessPhaseId, cancellationToken);
        
        if (processPhase == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.ProcessPhaseId));
        
        var processParameters = await _unitOfWork.ProcessParameters.GetAsync(x => request.ProcessParameterIds.Contains(x.Id), cancellationToken);
        
        if (processParameters.Count != request.ProcessParameterIds.Length)
            throw new ValidationException("One or more process parameters ids are invalid");

        var processPhaseParametersToAdd = processParameters.Select(x => new ProcessPhaseParameter()
        {
            ParameterId = x.Id,
            Parameter = x,
            PhaseId = request.ProcessPhaseId,
            Phase = processPhase,
        });

        await _unitOfWork.ProcessPhaseParameters.AddRangeAsync(processPhaseParametersToAdd, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveParametersAsync(RemoveParametersFromPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var processPhase = await _unitOfWork.ProcessPhases.GetByIdAsync(request.ProcessPhaseId, cancellationToken);
        
        if (processPhase == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.ProcessPhaseId));
        
        var processPhasesParameters = await _unitOfWork.ProcessPhaseParameters.GetAsync(x => request.ProcessParameterIds.Contains(x.ParameterId) && x.PhaseId == request.ProcessPhaseId, cancellationToken);
        
        if (processPhasesParameters.Count != request.ProcessParameterIds.Length)
            throw new ValidationException("One or more process parameters ids are invalid");

        _unitOfWork.ProcessPhaseParameters.RemoveRange(processPhasesParameters);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<ProcessPhase, bool>>? CreateFilterPredicate(GetProcessPhasesRequest request)
    {
        Expression<Func<ProcessPhase, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}