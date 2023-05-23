using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.Roles;

namespace WineQuality.Application.Models.Results.Users;

public class UserResult : BaseResult
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<RoleResult> Roles { get; set; } = null!;
    public string SelectedCulture { get; set; } = null!;
}