using AutoMapper;
using WineQuality.Application.Models.Requests.ProcessParameters;
using WineQuality.Application.Models.Results.ProcessParameters;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class ProcessParameterProfile : Profile
{
    public ProcessParameterProfile()
    {
        CreateMap<CreateProcessParameterRequest, ProcessParameter>();
        CreateMap<UpdateProcessParameterRequest, ProcessParameter>();
        CreateMap<ProcessParameter, ProcessParameterResult>()
            .ForMember(d => d.Phases,
                o => o.MapFrom(s => (s.Phases ?? new List<ProcessPhaseParameter>()).Select(p => p.PhaseType)));
    }
}