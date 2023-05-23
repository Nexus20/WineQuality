using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WineQuality.Application;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Domain.Enums;

namespace WineQuality.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SensorController : ControllerBase
{
    private readonly IProcessPhaseParameterSensorService _processPhaseParameterSensorService;
    private readonly IStringLocalizer<SharedResource> _stringLocalizer;

    /// <summary>
    /// Preferable DI constructor.
    /// </summary>
    /// <param name="processPhaseParameterSensorService">For sensors management.</param>
    /// <param name="stringLocalizer"></param>
    public SensorController(IProcessPhaseParameterSensorService processPhaseParameterSensorService, IStringLocalizer<SharedResource> stringLocalizer)
    {
        _processPhaseParameterSensorService = processPhaseParameterSensorService;
        _stringLocalizer = stringLocalizer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]GetProcessPhaseParameterSensorsRequest request, CancellationToken cancellationToken)
    {
        var result = await _processPhaseParameterSensorService.GetAsync(request, cancellationToken: cancellationToken);

        return Ok(result);
    }

    // GET: api/GrapeSort/5
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
    {
        var result = await _processPhaseParameterSensorService.GetByIdAsync(id, cancellationToken: cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _processPhaseParameterSensorService.DeleteSensorAsync(id, cancellationToken: cancellationToken);

        return NoContent();
    }
    
    // POST: api/GrapeSort
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProcessPhaseParameterSensorRequest request)
    {
        var result = await _processPhaseParameterSensorService.CreateSensorAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPost("assign_devices_to_wine_material_batch")]
    public async Task<IActionResult> AssignDevicesToWineMaterialBatch([FromBody] AssignDevicesToWineMaterialBatchRequest request)
    {
        await _processPhaseParameterSensorService.AssignDeviceToWineMaterialBatchAsync(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("{id}/run")]
    public async Task<IActionResult> RunSensor(string id)
    {
        await _processPhaseParameterSensorService.RunSensorAsync(id);
        return Accepted();
    }

    [HttpPost("{id}/stop")]
    public async Task<IActionResult> StopSensor(string id)
    {
        await _processPhaseParameterSensorService.StopSensorAsync(id);
        return Accepted();
    }

    [HttpPost("{wineMaterialBatchGrapeSortPhaseId}/run_by_phase")]
    public async Task<IActionResult> RunSensorsByPhase(string wineMaterialBatchGrapeSortPhaseId, CancellationToken cancellationToken)
    {
        await _processPhaseParameterSensorService.RunAllPhaseSensorsAsync(wineMaterialBatchGrapeSortPhaseId, cancellationToken);
        return Accepted();
    }
    
    [HttpPost("{wineMaterialBatchGrapeSortPhaseId}/stop_by_phase")]
    public async Task<IActionResult> StopSensorsByPhase(string wineMaterialBatchGrapeSortPhaseId, CancellationToken cancellationToken)
    {
        await _processPhaseParameterSensorService.StopAllPhaseSensorsAsync(wineMaterialBatchGrapeSortPhaseId, cancellationToken);
        return Accepted();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("statuses")]
    [ProducesResponseType(typeof(IDictionary<DeviceStatus, string>), 200)]
    public IActionResult GetStatuses()
    {
        var localizedStatuses = Enum.GetValues<DeviceStatus>()
            .OrderBy(s => s)
            .Select(s => new
            {
                Status = s,
                Value = _stringLocalizer[$"{nameof(DeviceStatus)}_{(int)s}"].Value
            });

        return Ok(localizedStatuses);
    }
}