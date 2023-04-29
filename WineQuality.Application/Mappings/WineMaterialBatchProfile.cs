using AutoMapper;
using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.WineMaterialBatches;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class WineMaterialBatchProfile : Profile
{
    public WineMaterialBatchProfile()
    {
        CreateMap<CreateWineMaterialBatchRequest, WineMaterialBatch>();
        CreateMap<UpdateWineMaterialBatchRequest, WineMaterialBatch>();
        CreateMap<AssignWineMaterialBatchToPhaseRequest, WineMaterialBatchProcessPhase>();
        CreateMap<WineMaterialBatch, WineMaterialBatchResult>()
            .ForMember(d => d.GrapeSort,
                o => o.MapFrom(s => s.GrapeSort))
            .ForMember(d => d.Phases,
                o => o.MapFrom(s => s.Phases));

        CreateMap<WineMaterialBatchProcessPhase, WineMaterialBatchProcessPhaseResult>()
            .ForMember(d => d.PhaseType,
                o => o.MapFrom(s => s.PhaseType));
    }
}