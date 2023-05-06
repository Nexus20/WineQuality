using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Requests.GrapeSorts;

namespace WineQuality.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DatasetsController : ControllerBase
{
    private readonly IDatasetService _datasetService;

    public DatasetsController(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    [HttpGet("{grapeSortPhaseId}")]
    public async Task<IActionResult> GetDatasets(string grapeSortPhaseId, CancellationToken cancellationToken)
    {
        var datasets = await _datasetService.GetGrapeSortPhaseDatasetsAsync(grapeSortPhaseId, cancellationToken);

        if (!datasets.Any())
            return NoContent();

        return Ok(datasets);
    }

    [HttpPost]
    public async Task<IActionResult> AddPhaseForecastModelDatasets([FromForm] AddGrapeSortPhaseForecastModelDatasetsRequest request)
    {
        if (!Request.Form.Files.Any())
            return BadRequest();
        
        var filesDtos = new List<FileDto>();

        foreach (var formFile in Request.Form.Files)
        {
            filesDtos.Add(new FileDto()
            {
                Content = formFile.OpenReadStream(),
                Name = formFile.FileName,
                ContentType = formFile.ContentType
            });
        }
            
        var result = await _datasetService.AddPhaseForecastModelDatasetsAsync(request, filesDtos);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _datasetService.DeleteAsync(id);
        return NoContent();
    }
}