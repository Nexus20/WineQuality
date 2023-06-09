using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Requests.GrapeSorts.Standards;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GrapeSortController : ControllerBase
{
    private readonly IGrapeSortService _grapeSortService;
    private readonly IGrapeSortStandardService _grapeSortStandardService;
    
    // GET: api/GrapeSort
    public GrapeSortController(IGrapeSortService grapeSortService, IGrapeSortStandardService grapeSortStandardService)
    {
        _grapeSortService = grapeSortService;
        _grapeSortStandardService = grapeSortStandardService;
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

    [HttpGet("{id}/phases")]
    public async Task<IActionResult> GetPhases(string id, CancellationToken cancellationToken)
    {
        var result = await _grapeSortService.GetPhasesByGrapeSortIdAsync(id, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("phases/{id}")]
    public async Task<IActionResult> GetPhaseById(string id, CancellationToken cancellationToken)
    {
        var result = await _grapeSortService.GetPhaseByIdAsync(id, cancellationToken);

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
    
    // PUT: api/GrapeSort/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}/save_phases_order")]
    public async Task<IActionResult> SavePhasesOrder(string id, [FromBody] SaveGrapeSortPhasesOrderRequest request)
    {
        if (id != request.GrapeSortId)
            return BadRequest();
        
        await _grapeSortService.SaveGrapeSortPhasesOrderAsync(request);
        return NoContent();
    }

    // DELETE: api/GrapeSort/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _grapeSortService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpPost("assign_phase")]
    // public async Task<IActionResult> AssignPhase([FromBody] AssignGrapeSortToPhaseRequest request)
    // {
    //     if (request.Order < 1)
    //         return BadRequest(new ErrorDetails("Phase order can't be lower than 1"));
    //     
    //     await _grapeSortService.AssignGrapeSortToPhaseAsync(request);
    //     return StatusCode(StatusCodes.Status201Created);
    // }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create_standard")]
    public async Task<IActionResult> CreateStandard([FromBody] CreateGrapeSortProcessPhaseParameterStandardRequest request)
    {
        if (request.LowerBound >= request.UpperBound)
            return BadRequest("Parameter lower bound can't be greater or equal than upper bound");
        
        var result = await _grapeSortStandardService.CreateGrapeSortPhaseParameterStandardAsync(request);
        
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpGet("standards/{standardId}")]
    public async Task<IActionResult> GetStandardById(string standardId, CancellationToken cancellationToken)
    {
        var result = await _grapeSortStandardService.GetGrapeSortPhaseParameterStandardByIdAsync(standardId, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPut("standards/{standardId}/edit")]
    public async Task<IActionResult> UpdateStandardById(string standardId, [FromBody]UpdateGrapeSortProcessPhaseParameterStandardsRequestPart request, CancellationToken cancellationToken)
    {
        if (standardId != request.StandardId)
            return BadRequest();
        
        await _grapeSortStandardService.UpdateGrapeSortPhaseParameterStandardAsync(request, cancellationToken);
        
        return NoContent();
    }
    
    [HttpDelete("standards/{standardId}/delete")]
    public async Task<IActionResult> DeleteStandardById(string standardId, CancellationToken cancellationToken)
    {
        await _grapeSortStandardService.DeleteGrapeSortPhaseParameterStandardByIdAsync(standardId, cancellationToken);
        
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update_standards")]
    public async Task<IActionResult> UpdateStandards(
        [FromBody] UpdateGrapeSortProcessPhaseParameterStandardsRequest request)
    {
        if(request.Standards.Any(x => x.LowerBound >= x.UpperBound))
            return BadRequest("Parameter lower bound can't be greater or equal than upper bound");
        
        await _grapeSortStandardService.UpdateGrapeSortPhaseParameterStandardsAsync(request);
        
        return NoContent();
    }
}