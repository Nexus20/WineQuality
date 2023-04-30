using AutoMapper;
using WineQuality.Application.Models.Requests.GrapeSorts.Standards;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class GrapeSortProcessPhaseParameterStandardProfile : Profile
{
    public GrapeSortProcessPhaseParameterStandardProfile()
    {
        CreateMap<GrapeSortProcessPhaseParameterStandard, GrapeSortProcessPhaseParameterStandardResult>()
            .ForMember(d => d.GrapeSortName, o => o.MapFrom(s => s.GrapeSortPhase.GrapeSort.Name))
            .ForMember(d => d.ParameterName, o => o.MapFrom(s => s.PhaseParameter.Parameter.Name))
            .ForMember(d => d.PhaseName, o => o.MapFrom(s => s.GrapeSortPhase.Phase.Name))
            .MaxDepth(2);
        CreateMap<CreateGrapeSortProcessPhaseParameterStandardRequest, GrapeSortProcessPhaseParameterStandard>();
    }
}