using Microsoft.AspNetCore.Identity;
using WineQuality.Application.Authorization;
using WineQuality.Application.Interfaces.Infrastructure;

namespace WineQuality.Infrastructure.Identity;

public class IdentityInitializer : IIdentityInitializer
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public IdentityInitializer(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeIdentityData()
    {
        InitializeSuperAdminRole().Wait();
        RegisterRoleAsync(CustomRoles.Admin).Wait();
        RegisterRoleAsync(CustomRoles.User).Wait();
    }
    
    private async Task<AppRole> RegisterRoleAsync(string roleName)
    {

        var role  = await _roleManager.FindByNameAsync(roleName);

        if (role != null) {
            return role;
        }

        role = new AppRole(roleName);
        await _roleManager.CreateAsync(role);

        return role;
    }
    
    private async Task InitializeSuperAdminRole() {

        var superAdmin = _userManager.Users.FirstOrDefault(u => u.UserName == "root@wine-quality.com") ?? RegisterSuperAdmin();

        var superAdminRole = await RegisterRoleAsync(CustomRoles.SuperAdmin);

        if(!await _userManager.IsInRoleAsync(superAdmin, CustomRoles.SuperAdmin))
            await _userManager.AddToRoleAsync(superAdmin, superAdminRole.Name);
    }
    
    private AppUser RegisterSuperAdmin() {

        var superAdmin = new AppUser() {
            Id = Guid.NewGuid().ToString(),
            UserName = "root@wine-quality.com",
            Email = "root@wine-quality.com",
            FirstName = "root",
            LastName = "root",
            PhoneNumber = "000",
            SelectedCulture = "en-US",
        };

        _userManager.CreateAsync(superAdmin, "_QGrXyvcmTD4aVQJ_").Wait();
        return superAdmin;
    }
}