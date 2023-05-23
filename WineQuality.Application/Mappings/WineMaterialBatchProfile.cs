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
        CreateMap<AssignWineMaterialBatchToPhasesRequest, WineMaterialBatchGrapeSortPhase>();
        CreateMap<WineMaterialBatch, WineMaterialBatchResult>()
            .ForMember(d => d.GrapeSort,
                o => o.MapFrom(s => s.GrapeSort))
            .ForMember(d => d.Phases,
                o => o.MapFrom(s => s.Phases));

        CreateMap<WineMaterialBatch, WineMaterialBatchDetailsResult>()
            .ForMember(d => d.GrapeSort,
                o => o.MapFrom(s => s.GrapeSort))
            .ForMember(d => d.Phases,
                o => o.MapFrom(s => s.Phases))
            .ForMember(d => d.ActivePhase, o => o.MapFrom(s => s.Phases.FirstOrDefault(p => p.IsActive)));

        CreateMap<WineMaterialBatchGrapeSortPhase, WineMaterialBatchGrapeSortPhaseResult>()
            .ForMember(d => d.Phase,
                o => o.MapFrom(s => s.GrapeSortPhase));
        
        CreateMap<WineMaterialBatchGrapeSortPhase, ActiveWineMaterialBatchGrapeSortPhaseResult>()
            .ForMember(d => d.Phase,
                o => o.MapFrom(s => s.GrapeSortPhase))
            .ForMember(d => d.Readings, o => o.MapFrom(s => s.Parameters));

        CreateMap<WineMaterialBatchGrapeSortPhaseParameter, WineMaterialBatchProcessPhaseReadingsResult>()
            .ForMember(d => d.Value, o => o.MapFrom(s => s.Values == null ? -1.0 : s.Values.MaxBy(v => v.CreatedAt).Value))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.Values == null ? DateTime.MinValue : s.Values.MaxBy(v => v.CreatedAt).CreatedAt))
            .ForMember(d => d.ParameterId, o => o.MapFrom(s => s.PhaseParameter.Parameter.Id))
            .ForMember(d => d.ParameterName, o => o.MapFrom(s => s.PhaseParameter.Parameter.Name));

        CreateMap<WineMaterialBatchGrapeSortPhaseParameter, WineMaterialBatchGrapeSortPhaseParameterResult>();
        // .ForMember(d => d.Parameter, o => o.MapFrom(s => s.PhaseParameter.Parameter));

        CreateMap<WineMaterialBatchGrapeSortPhaseParameterValue, WineMaterialBatchGrapeSortPhaseParameterValueResult>();

        CreateMap<WineMaterialBatchGrapeSortPhaseParameter, WineMaterialBatchGrapeSortPhaseParameterDetailsResult>()
            .ForMember(d => d.Parameter, o => o.MapFrom(s => s.PhaseParameter.Parameter))
            .ForMember(d => d.Sensors, o => o.MapFrom(s => s.Sensors));

        CreateMap<WineMaterialBatchGrapeSortPhase, WineMaterialBatchGrapeSortPhaseDetailsResult>()
            .ForMember(d => d.Phase,
                o => o.MapFrom(s => s.GrapeSortPhase))
            .ForMember(d => d.ParametersDetails,
                o => o.MapFrom(s => s.Parameters));
    }
}