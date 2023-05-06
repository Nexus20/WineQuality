using AutoMapper;
using WineQuality.Application.Models.Results.Roles;
using WineQuality.Infrastructure.Identity;

namespace WineQuality.Infrastructure.Mappings;

public class AppRoleProfile : Profile
{
    public AppRoleProfile()
    {
        CreateMap<AppRole, RoleResult>();
    }
}