using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.WineMaterialBatches;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WineMaterialBatchController : ControllerBase
{
    private readonly IWineMaterialBatchService _wineMaterialBatchService;
    
    // GET: api/WineMaterialBatch
    public WineMaterialBatchController(IWineMaterialBatchService wineMaterialBatchService)
    {
        _wineMaterialBatchService = wineMaterialBatchService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetWineMaterialBatchesRequest request, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/WineMaterialBatch/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    // POST: api/WineMaterialBatch
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWineMaterialBatchRequest request)
    {
        var result = await _wineMaterialBatchService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/WineMaterialBatch/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateWineMaterialBatchRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _wineMaterialBatchService.UpdateAsync(request);
        return Ok(result);
    }

    // DELETE: api/WineMaterialBatch/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _wineMaterialBatchService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("assign_phase")]
    public async Task<IActionResult> AssignPhase([FromBody] AssignWineMaterialBatchToPhaseRequest request)
    {
        if (request.StartDate >= request.EndDate)
            return BadRequest(new ErrorDetails("Phase start date can't be greater than phase end date"));
        
        await _wineMaterialBatchService.AssignWineMaterialBatchToPhaseAsync(request);
        return StatusCode(StatusCodes.Status201Created);
    }
}