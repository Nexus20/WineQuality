using AutoMapper;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Mappings;

public class GrapeSortProfile : Profile
{
    public GrapeSortProfile()
    {
        CreateMap<CreateGrapeSortRequest, ProcessPhaseParameter>();
        CreateMap<UpdateGrapeSortRequest, ProcessPhaseParameter>();
        CreateMap<GrapeSort, GrapeSortResult>();
    }
}