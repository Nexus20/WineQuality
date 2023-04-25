using Microsoft.AspNetCore.Identity;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Infrastructure.Identity;

public class AppUser : IdentityUser, ITimeMarkedEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual List<AppUserRole> UserRoles { get; set; }
}