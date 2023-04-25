using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GrapeSortController : ControllerBase
{
    private readonly IGrapeSortService _grapeSortService;
    
    // GET: api/GrapeSort
    public GrapeSortController(IGrapeSortService grapeSortService)
    {
        _grapeSortService = grapeSortService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetGrapeSortsRequest request, CancellationToken cancellationToken)
    {
        var result = await _grapeSortService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/GrapeSort/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _grapeSortService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    // POST: api/GrapeSort
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateGrapeSortRequest request)
    {
        var result = await _grapeSortService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/GrapeSort/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateGrapeSortRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _grapeSortService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/GrapeSort/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _grapeSortService.DeleteAsync(id);
        return NoContent();
    }
}