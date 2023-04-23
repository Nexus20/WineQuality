using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.Users;

public class ProfileResult : BaseResult
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
}