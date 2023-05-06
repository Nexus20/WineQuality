using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Authorization;
using WineQuality.Application.Interfaces.Services.Identity;
using WineQuality.Application.Models.Requests.Roles;

namespace WineQuality.API.Controllers;

/// <summary>
/// Controller with role management related endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = CustomRoles.Admin)]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
        
    
    /// <summary>
    /// Preferable DI constructor
    /// </summary>
    /// <param name="roleService">Operations with roles.</param>
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    /// <summary>
    /// Gets roles list.
    /// </summary>
    /// <param name="cancellationToken">For operation cancellation.</param>
    /// <returns>
    /// Task that gets:
    /// <list type="bullet">
    /// <item><see cref="OkObjectResult"/> with value <see cref="List{RoleResult}"/> that represents roles list.</item>
    /// <item><see cref="NoContentResult"/> <b>When</b> no roles in the system.</item>
    /// </list>
    /// </returns>
    /// <response code="200">Returns roles list.</response>
    /// <response code="204">When there are no roles in the system.</response>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAsync(cancellationToken);

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
    
    /// <summary>
    /// Gets role by its id.
    /// </summary>
    /// <param name="id">Role identifier in GUID format.</param>
    /// <param name="cancellationToken">For operation cancellation.</param>
    /// <returns>
    /// <list type="bullet">
    /// <item><see cref="OkObjectResult"/> <b>When</b> the user with such id is present in the system.</item>
    /// <item><see cref="BadRequestResult"/> <b>When</b> there is no user with such id.</item>
    /// </list>
    /// </returns>
    /// <response code="200">Returns user.</response>
    /// <response code="400">When there is no user with such id.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _roleService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/Role
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoleRequest request)
    {
        var result = await _roleService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/Role/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/Role/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}