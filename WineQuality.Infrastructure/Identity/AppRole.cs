using Microsoft.AspNetCore.Identity;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Infrastructure.Identity;

public class AppRole : IdentityRole, ITimeMarkedEntity
{
    public AppRole()
    {
        
    }
    
    public AppRole(string roleName) : base(roleName) { }
    
    public virtual List<AppUserRole> UserRoles { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}