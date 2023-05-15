using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProcessPhaseController : ControllerBase
{
    private readonly IProcessPhaseService _processPhaseService;
    
    // GET: api/ProcessPhase
    public ProcessPhaseController(IProcessPhaseService processPhaseService)
    {
        _processPhaseService = processPhaseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetProcessPhasesRequest request, CancellationToken cancellationToken)
    {
        var result = await _processPhaseService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/ProcessPhase/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _processPhaseService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    // POST: api/ProcessPhase
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProcessPhaseRequest request)
    {
        var result = await _processPhaseService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/ProcessPhase/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateProcessPhaseRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _processPhaseService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/ProcessPhase/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _processPhaseService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("add_parameters")]
    public async Task<IActionResult> AddParametersToPhase([FromBody] AddParametersToPhaseRequest request)
    {
        await _processPhaseService.AddParametersAsync(request);
        return NoContent();
    }
    
    [HttpPost("remove_parameters")]
    public async Task<IActionResult> RemoveParametersFromPhase([FromBody] RemoveParametersFromPhaseRequest request)
    {
        await _processPhaseService.RemoveParametersAsync(request);
        return NoContent();
    }
}