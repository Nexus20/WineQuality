using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseTypes;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProcessPhaseTypeController : ControllerBase
{
    private readonly IProcessPhaseTypeService _processPhaseTypeService;
    
    // GET: api/ProcessPhaseType
    public ProcessPhaseTypeController(IProcessPhaseTypeService processPhaseTypeService)
    {
        _processPhaseTypeService = processPhaseTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetProcessPhaseTypesRequest request, CancellationToken cancellationToken)
    {
        var result = await _processPhaseTypeService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/ProcessPhaseType/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _processPhaseTypeService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    // POST: api/ProcessPhaseType
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProcessPhaseTypeRequest request)
    {
        var result = await _processPhaseTypeService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/ProcessPhaseType/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateProcessPhaseTypeRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _processPhaseTypeService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/ProcessPhaseType/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _processPhaseTypeService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("add_parameter")]
    public async Task<IActionResult> AddParameterToPhase([FromBody] AddParameterToPhaseRequest request)
    {
        await _processPhaseTypeService.AddParameterAsync(request);
        return Ok();
    }
}