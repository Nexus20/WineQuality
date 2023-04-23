using Microsoft.AspNetCore.Identity;
using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Infrastructure.Identity;

public class AppUserRole : IdentityUserRole<string>, ITimeMarkedEntity {

    public virtual AppUser User { get; set; }

    public virtual AppRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}