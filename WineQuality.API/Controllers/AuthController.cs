using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.Auth;
using WineQuality.Application.Models.Results.Auth;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISignInService _signInService;

    public AuthController(ISignInService signInService)
    {
        _signInService = signInService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _signInService.SignInAsync(request);
        return result.IsAuthSuccessful ? Ok(result) : Unauthorized(result);
    }
}