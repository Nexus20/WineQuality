using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Authorization;
using WineQuality.Application.Interfaces.Services.Identity;
using WineQuality.Application.Models.Requests.Users;

namespace WineQuality.API.Controllers;

/// <summary>
/// Controller with user management related endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{CustomRoles.Admin}, {CustomRoles.SuperAdmin}")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    /// <summary>
    /// Preferable DI constructor.
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Get all users.
    /// </summary>
    /// <param name="cancellationToken">For operation cancellation.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAsync(cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    // GET: api/User/5
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/User
    /// <summary>
    /// Create new user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var result = await _userService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/User/5
    /// <summary>
    /// Update user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateUserRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _userService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/User/5
    /// <summary>
    /// Delete user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}