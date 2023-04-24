using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessParameters;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProcessParameterController : ControllerBase
{
    private readonly IProcessParameterService _processParameterService;
    
    // GET: api/ProcessParameter
    public ProcessParameterController(IProcessParameterService processParameterService)
    {
        _processParameterService = processParameterService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetProcessParametersRequest request, CancellationToken cancellationToken)
    {
        var result = await _processParameterService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/ProcessParameter/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _processParameterService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    // POST: api/ProcessParameter
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProcessParameterRequest request)
    {
        var result = await _processParameterService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/ProcessParameter/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateProcessParameterRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _processParameterService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/ProcessParameter/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _processParameterService.DeleteAsync(id);
        return NoContent();
    }
}