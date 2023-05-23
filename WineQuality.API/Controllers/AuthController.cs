using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.Auth;
using WineQuality.Application.Models.Results.Auth;

namespace WineQuality.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISignInService _signInService;

    /// <summary>
    /// Preferable DI constructor.
    /// </summary>
    /// <param name="signInService"></param>
    public AuthController(ISignInService signInService)
    {
        _signInService = signInService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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