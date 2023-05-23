using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.WineMaterialBatches;

namespace WineQuality.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WineMaterialBatchController : ControllerBase
{
    private readonly IWineMaterialBatchService _wineMaterialBatchService;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wineMaterialBatchService"></param>
    public WineMaterialBatchController(IWineMaterialBatchService wineMaterialBatchService)
    {
        _wineMaterialBatchService = wineMaterialBatchService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetWineMaterialBatchesRequest request, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/WineMaterialBatch/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }
    
    // GET: api/WineMaterialBatch/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetDetails(string id, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetDetailsByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="phaseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("phases/{phaseId}/details")]
    public async Task<IActionResult> GetPhaseDetails(string phaseId, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetPhaseDetailsByIdAsync(phaseId, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWineMaterialBatchRequest request)
    {
        var result = await _wineMaterialBatchService.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    // PUT: api/WineMaterialBatch/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateWineMaterialBatchRequest request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _wineMaterialBatchService.UpdateAsync(request);
        return Ok(result);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _wineMaterialBatchService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("assign_phases")]
    public async Task<IActionResult> AssignPhases([FromBody] AssignWineMaterialBatchToPhasesRequest request)
    {
        if (request.Phases.Any(r => r.StartDate >= r.EndDate))
            return BadRequest(new ErrorDetails("Phase start date can't be greater than phase end date"));
        
        await _wineMaterialBatchService.AssignWineMaterialBatchToPhasesAsync(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}/start_allowed")]
    public async Task<IActionResult> CheckIfProcessStartAllowedAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.CheckIfProcessStartAllowedAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("phases/{phaseId}/start_allowed")]
    public async Task<IActionResult> CheckIfPhaseStartAllowedAsync(string phaseId, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.CheckIfPhaseStartAllowedAsync(phaseId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("run_process_phase")]
    public async Task<IActionResult> RunProcessForPhase([FromBody] ChangeWineProcessingRequestRunningState request)
    {
        await _wineMaterialBatchService.RunWineProcessingForPhaseAsync(request);

        return Accepted();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("stop_process_phase")]
    public async Task<IActionResult> StopProcessForPhase([FromBody] ChangeWineProcessingRequestRunningState request)
    {
        await _wineMaterialBatchService.StopWineProcessingForPhaseAsync(request);

        return Accepted();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update_terms")]
    public async Task<IActionResult> UpdatePhasesTerms([FromBody] UpdateWineMaterialBatchPhasesTermsRequest request)
    {
        if (request.Terms.Any(x => x.StartDate >= x.EndDate))
            return BadRequest("Phase start date can't be equal or greater than end date");

        for (var i = 1; i < request.Terms.Count; i++)
        {
            if (request.Terms[i].StartDate < request.Terms[i - 1].EndDate)
                return BadRequest("Next phase start date can't be less than previous phase end date");
        }

        await _wineMaterialBatchService.UpdatePhasesTermsAsync(request);
        
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("phases/parameters/get_chart_data")]
    public async Task<IActionResult> GetPhaseParametersChart([FromQuery] GetWineMaterialBatchPhaseParameterChartDataRequest request, CancellationToken cancellationToken)
    {
        var result = await _wineMaterialBatchService.GetPhaseParametersChartAsync(request, cancellationToken);
        
        return Ok(result);
    }
}