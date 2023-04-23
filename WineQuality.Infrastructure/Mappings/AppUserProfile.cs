using AutoMapper;
using WineQuality.Application.Models.Results.Users;
using WineQuality.Infrastructure.Identity;

namespace WineQuality.Infrastructure.Mappings;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<AppUser, ProfileResult>()
            .ForMember(d => d.Phone, o => o.MapFrom(s => s.PhoneNumber));
    }
}