using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Interfaces.Services;

public interface IDatasetService
{
    Task<List<GrapeSortPhaseDatasetResult>> GetGrapeSortPhaseDatasetsAsync(string grapeSortPhaseId,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    
    Task<List<GrapeSortPhaseDatasetResult>> AddPhaseForecastModelDatasetsAsync(AddGrapeSortPhaseForecastModelDatasetsRequest request, List<FileDto> filesDtos, CancellationToken cancellationToken = default);
}