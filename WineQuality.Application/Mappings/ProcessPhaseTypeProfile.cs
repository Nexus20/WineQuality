using AutoMapper;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class ProcessPhaseTypeProfile : Profile
{
    public ProcessPhaseTypeProfile()
    {
        CreateMap<CreateProcessPhaseTypeRequest, ProcessPhaseType>();
        CreateMap<UpdateProcessPhaseTypeRequest, ProcessPhaseType>();
        CreateMap<ProcessPhaseType, ProcessPhaseTypeResult>()
            .ForMember(d => d.Parameters,
                o => o.MapFrom(s => (s.Parameters ?? new List<ProcessPhaseParameter>()).Select(p => p.Parameter)));
    }
}