using Microsoft.AspNetCore.Mvc;
using WineQuality.Application.Interfaces.Services;

namespace WineQuality.API.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CultureController : ControllerBase
{
    private readonly ICultureService _cultureService;

    public CultureController(ICultureService cultureService)
    {
        _cultureService = cultureService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _cultureService.GetAllAsync(cancellationToken: cancellationToken);

        return Ok(result);
    }
}