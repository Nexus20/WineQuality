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
        CreateMap<WineMaterialBatch, WineMaterialBatchResult>()
            .ForMember(d => d.GrapeSort,
                o => o.MapFrom(s => s.GrapeSort));
    }
}