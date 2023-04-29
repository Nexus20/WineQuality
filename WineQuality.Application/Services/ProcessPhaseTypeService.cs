﻿using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;
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
        var source = await _unitOfWork.ProcessPhases.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(id));

        var result = _mapper.Map<ProcessPhase, ProcessPhaseResult>(source);

        return result;
    }

    public async Task<List<ProcessPhaseResult>> GetAsync(GetProcessPhasesRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.ProcessPhases.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<ProcessPhaseResult>();

        var result = _mapper.Map<List<ProcessPhase>, List<ProcessPhaseResult>>(source);

        // TODO: Move order by into querying data
        return result.OrderBy(x => x.Order).ToList();
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
        
        _unitOfWork.ProcessPhases.Delete(processPhaseToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddParameterAsync(AddParameterToPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var processPhase = await _unitOfWork.ProcessPhases.GetByIdAsync(request.ProcessPhaseId, cancellationToken);
        
        if (processPhase == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.ProcessPhaseId));
        
        var processParameter = await _unitOfWork.ProcessParameters.GetByIdAsync(request.ProcessParameterId, cancellationToken);
        
        if (processParameter == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(request.ProcessParameterId));

        var processPhaseParameterToAdd = new ProcessPhaseParameter()
        {
            ParameterId = request.ProcessParameterId,
            Parameter = processParameter,
            PhaseId = request.ProcessPhaseId,
            Phase = processPhase,
        };

        await _unitOfWork.ProcessPhaseParameters.AddAsync(processPhaseParameterToAdd, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<ProcessPhase, bool>>? CreateFilterPredicate(GetProcessPhasesRequest request)
    {
        Expression<Func<ProcessPhase, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}