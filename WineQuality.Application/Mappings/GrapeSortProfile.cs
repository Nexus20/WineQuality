using AutoMapper;
using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class GrapeSortProfile : Profile
{
    public GrapeSortProfile()
    {
        CreateMap<CreateGrapeSortRequest, GrapeSort>();
        CreateMap<UpdateGrapeSortRequest, GrapeSort>();
        CreateMap<GrapeSort, GrapeSortResult>();
        CreateMap<GrapeSort, GrapeSortDetailsResult>();
        CreateMap<AssignGrapeSortToPhaseRequest, GrapeSortPhase>();

        CreateMap<GrapeSortPhase, GrapeSortPhaseResult>()
            .ForMember(d => d.Parameters, o => o.MapFrom(s => (s.Phase.Parameters ?? new List<ProcessPhaseParameter>()).Select(x => x.Parameter)))
            .ForMember(d => d.PhaseName, o => o.MapFrom(s => s.Phase.Name))
            .ForMember(d => d.GrapeSortProcessPhaseParameterStandards,
                o => o.MapFrom(s => s.GrapeSortProcessPhaseParameterStandards));

        CreateMap<GrapeSortPhaseDataset, GrapeSortPhaseDatasetResult>()
            .ForMember(d => d.DatasetInfo, o => o.MapFrom(s => new FileNameWithUrlDto
            {
                Url = s.DatasetFileReference.Uri,
                Name = s.Name
            }));

        CreateMap<GrapeSortPhaseForecastModel, GrapeSortPhaseForecastModelResult>()
            .ForMember(d => d.ModelUri, o => o.MapFrom(s => s.ForecastingModelFileReference.Uri));
    }
}