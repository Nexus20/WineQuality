using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;

namespace WineQuality.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class QualityPredictionController : ControllerBase
{
    private readonly IQualityPredictionService _qualityPredictionService;

    public QualityPredictionController(IQualityPredictionService qualityPredictionService)
    {
        _qualityPredictionService = qualityPredictionService;
    }

    [HttpPost("train_phase_model")]
    public async Task<IActionResult> TrainPhaseModel([FromBody] TrainPhaseModelRequest request)
    {
        var result = await _qualityPredictionService.TrainModelByDatasetIdAsync(request);

        return Ok(result);
    }
    
    [HttpGet("{grapeSortPhaseId}/models")]
    public async Task<IActionResult> GetModels(string grapeSortPhaseId, CancellationToken cancellationToken)
    {
        var datasets = await _qualityPredictionService.GetGrapeSortPhaseForecastModelsAsync(grapeSortPhaseId, cancellationToken);

        if (!datasets.Any())
            return NoContent();

        return Ok(datasets);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="grapeSortPhaseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{grapeSortPhaseId}/history")]
    public async Task<IActionResult> GetPredictionsHistory(string grapeSortPhaseId, CancellationToken cancellationToken)
    {
        var history = await _qualityPredictionService.GetGrapeSortPhasePredictionsHistoryAsync(grapeSortPhaseId, cancellationToken);

        if (!history.Any())
            return NoContent();

        return Ok(history);
    }

    [HttpPost("predict")]
    public async Task<IActionResult> PredictQuality(PredictQualityRequest request)
    {
        var result = await _qualityPredictionService.PredictQualityAsync(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _qualityPredictionService.DeleteGrapeSortPhaseForecastModelAsync(id);
        return NoContent();
    }
}