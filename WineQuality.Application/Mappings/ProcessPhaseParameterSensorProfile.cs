using AutoMapper;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Application.Models.Results.ProcessPhaseParameterSensors;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class ProcessPhaseParameterSensorProfile : Profile
{
    public ProcessPhaseParameterSensorProfile()
    {
        CreateMap<ProcessPhaseParameterSensor, ProcessPhaseParameterSensorResult>()
            .ForMember(d => d.ParameterId, o => o.MapFrom(s => s.PhaseParameter.ParameterId))
            .ForMember(d => d.ParameterName, o => o.MapFrom(s => s.PhaseParameter.Parameter.Name))
            .ForMember(d => d.PhaseId, o => o.MapFrom(s => s.PhaseParameter.PhaseId))
            .ForMember(d => d.PhaseName, o => o.MapFrom(s => s.PhaseParameter.Phase.Name))
            .ForMember(d => d.WineBatchId,
                o => o.MapFrom(s =>
                    s.WineMaterialBatchGrapeSortPhaseParameter != null
                        ? s.WineMaterialBatchGrapeSortPhaseParameter.WineMaterialBatchGrapeSortPhase.WineMaterialBatchId
                        : null))
            .ForMember(d => d.WineBatchName,
                o => o.MapFrom(s =>
                    s.WineMaterialBatchGrapeSortPhaseParameter != null
                        ? s.WineMaterialBatchGrapeSortPhaseParameter.WineMaterialBatchGrapeSortPhase.WineMaterialBatch
                            .Name
                        : null));
    }
}