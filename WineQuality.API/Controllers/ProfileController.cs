using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services.Identity;
using WineQuality.Application.Models.Requests.Users;

namespace WineQuality.API.Controllers;

/// <summary>
/// Controller with user profile related endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfileController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Preferable DI constructor.
    /// </summary>
    /// <param name="userService"></param>
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/Profile
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        
        var result = await _userService.GetByIdAsync(userId, cancellationToken);
        return Ok(result);
    }

    // PUT: api/Profile/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        
        var updateUserRequest = new UpdateUserRequest
        {
            Id = userId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone
        };
        
        var result = await _userService.UpdateAsync(updateUserRequest, cancellationToken);
        return Ok(result);
    }
}