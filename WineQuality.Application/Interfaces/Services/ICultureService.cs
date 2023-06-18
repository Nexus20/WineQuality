using WineQuality.Application.Models.Results.Cultures;

namespace WineQuality.Application.Interfaces.Services;

public interface ICultureService
{
    Task<List<CultureResult>> GetAllAsync(CancellationToken cancellationToken = default);
}