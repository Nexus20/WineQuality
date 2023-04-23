using WineQuality.Application.Models.Requests.Auth;
using WineQuality.Application.Models.Results.Auth;

namespace WineQuality.Application.Interfaces.Services;

public interface ISignInService {

    Task<LoginResult> SignInAsync(LoginRequest request);
}