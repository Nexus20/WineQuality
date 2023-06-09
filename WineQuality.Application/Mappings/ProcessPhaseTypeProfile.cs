﻿using AutoMapper;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhases;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class ProcessPhaseProfile : Profile
{
    public ProcessPhaseProfile()
    {
        CreateMap<CreateProcessPhaseRequest, ProcessPhase>();
        CreateMap<UpdateProcessPhaseRequest, ProcessPhase>();
        CreateMap<ProcessPhase, ProcessPhaseResult>();
        
        CreateMap<ProcessPhase, ProcessPhaseDetailsResult>()
            .ForMember(d => d.Parameters,
                o => o.MapFrom(s => (s.Parameters ?? new List<ProcessPhaseParameter>()).Select(p => p.Parameter))).MaxDepth(2);
    }
}