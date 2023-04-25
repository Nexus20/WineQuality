using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessParameters;
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
        var source = await _unitOfWork.ProcessParameters.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(id));

        var result = _mapper.Map<ProcessParameter, ProcessParameterResult>(source);

        return result;
    }

    public async Task<List<ProcessParameterResult>> GetAsync(GetProcessParametersRequest request, CancellationToken cancellationToken = default)
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
        
        var processParameterToAdd = _mapper.Map<CreateProcessParameterRequest, ProcessParameter>(request);

        await _unitOfWork.ProcessParameters.AddAsync(processParameterToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessParameter, ProcessParameterResult>(processParameterToAdd);
        return result;
    }

    public async Task<ProcessParameterResult> UpdateAsync(UpdateProcessParameterRequest request, CancellationToken cancellationToken = default)
    {
        var processParameterToUpdate = await _unitOfWork.ProcessParameters.GetByIdAsync(request.Id, cancellationToken);
        
        if (processParameterToUpdate == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.ProcessParameters.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(ProcessParameter)} with such name {request.Name} already exists");

        _mapper.Map(request, processParameterToUpdate);

        _unitOfWork.ProcessParameters.Update(processParameterToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ProcessParameter, ProcessParameterResult>(processParameterToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var processParameterToDelete = await _unitOfWork.ProcessParameters.GetByIdAsync(id, cancellationToken);
        
        if (processParameterToDelete == null)
            throw new NotFoundException(nameof(ProcessParameter), nameof(id));
        
        _unitOfWork.ProcessParameters.Delete(processParameterToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private Expression<Func<ProcessParameter, bool>>? CreateFilterPredicate(GetProcessParametersRequest request)
    {
        Expression<Func<ProcessParameter, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}