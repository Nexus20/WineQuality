using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts.Standards;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class GrapeSortStandardService : IGrapeSortStandardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GrapeSortStandardService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GrapeSortProcessPhaseParameterStandardResult> CreateGrapeSortPhaseParameterStandardAsync(
        CreateGrapeSortProcessPhaseParameterStandardRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSortPhase = await _unitOfWork.GrapeSortPhases.GetByIdAsync(request.GrapeSortPhaseId, cancellationToken);
        
        if (grapeSortPhase == null)
            throw new NotFoundException(nameof(GrapeSortPhase), nameof(request.GrapeSortPhaseId));
        
        var phaseParameter = await _unitOfWork.ProcessPhaseParameters.FirstOrDefaultAsync(x => x.ParameterId == request.ParameterId && x.PhaseId == grapeSortPhase.PhaseId, cancellationToken);
        
        if (phaseParameter == null)
            throw new ValidationException($"Parameter with id {request.ParameterId} doesn't belong to phase with id {grapeSortPhase.PhaseId}");

        var entityToAdd = _mapper.Map<CreateGrapeSortProcessPhaseParameterStandardRequest, GrapeSortProcessPhaseParameterStandard>(request);
        entityToAdd.PhaseParameterId = phaseParameter.Id;
        entityToAdd.PhaseParameter = phaseParameter;
        
        await _unitOfWork.GrapeSortProcessPhaseParameterStandards.AddAsync(entityToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var source =
            await _unitOfWork.GrapeSortProcessPhaseParameterStandards.GetByIdAsync(entityToAdd.Id, cancellationToken);
        
        var result = _mapper.Map<GrapeSortProcessPhaseParameterStandard, GrapeSortProcessPhaseParameterStandardResult>(source!);

        return result;
    }

    public async Task UpdateGrapeSortPhaseParameterStandardsAsync(UpdateGrapeSortProcessPhaseParameterStandardsRequest request,
        CancellationToken cancellationToken = default)
    {
        var standardsIds = request.Standards.Select(x => x.StandardId);
        var standardsToUpdate = await _unitOfWork.GrapeSortProcessPhaseParameterStandards.GetAsync(x => standardsIds.Contains(x.Id), cancellationToken);

        if (standardsToUpdate.Count != standardsIds.Count())
            throw new ValidationException("One or more standards ids are invalid");
        
        standardsToUpdate.ForEach(x =>
        {
            var requestPart = request.Standards.Single(s => s.StandardId == x.Id);
            x.UpperBound = requestPart.UpperBound;
            x.LowerBound = requestPart.LowerBound;
            
            _unitOfWork.GrapeSortProcessPhaseParameterStandards.Update(x);
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}