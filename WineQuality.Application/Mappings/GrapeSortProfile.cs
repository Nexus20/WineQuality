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

        CreateMap<GrapeSortPhaseDataset, GrapeSortPhaseDatasetResult>()
            .ForMember(d => d.DatasetInfo, o => o.MapFrom(s => new FileNameWithUrlDto
            {
                Url = s.DatasetFileReference.Uri,
                Name = s.Name
            }));
    }
}