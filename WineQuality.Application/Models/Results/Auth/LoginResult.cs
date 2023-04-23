namespace WineQuality.Application.Models.Results.Auth;

public class LoginResult
{
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}