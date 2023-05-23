using AutoMapper;
using WineQuality.Application.Models.Requests.Users;
using WineQuality.Application.Models.Results.Users;
using WineQuality.Infrastructure.Identity;

namespace WineQuality.Infrastructure.Mappings;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<AppUser, UserResult>()
            .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(ur => ur.Role)));
        CreateMap<CreateUserRequest, AppUser>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email));
        CreateMap<UpdateUserRequest, AppUser>();
    }
}