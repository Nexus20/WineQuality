using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Authorization;
using WineQuality.Application.Interfaces.Services.Identity;
using WineQuality.Application.Models.Requests.Users;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = CustomRoles.Admin)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
        
    // GET: api/User
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAsync(cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var result = await _userService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}