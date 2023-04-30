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
        
        var phaseParameter = await _unitOfWork.ProcessPhaseParameters.GetByIdAsync(request.PhaseParameterId, cancellationToken);
        
        if (phaseParameter == null)
            throw new NotFoundException(nameof(ProcessPhaseParameter), nameof(request.PhaseParameterId));

        var entityToAdd = _mapper.Map<CreateGrapeSortProcessPhaseParameterStandardRequest, GrapeSortProcessPhaseParameterStandard>(request);
        
        await _unitOfWork.GrapeSortProcessPhaseParameterStandards.AddAsync(entityToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var source =
            await _unitOfWork.GrapeSortProcessPhaseParameterStandards.GetByIdAsync(entityToAdd.Id, cancellationToken);
        
        var result = _mapper.Map<GrapeSortProcessPhaseParameterStandard, GrapeSortProcessPhaseParameterStandardResult>(source!);

        return result;
    }
}