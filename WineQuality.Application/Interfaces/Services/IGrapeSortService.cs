using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.Services;

public interface IGrapeSortService {
    Task<GrapeSortResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<GrapeSortResult>> GetAsync(GetGrapeSortsRequest request, CancellationToken cancellationToken = default);
    Task<GrapeSortResult> CreateAsync(CreateGrapeSortRequest request, CancellationToken cancellationToken = default);
    Task<GrapeSortResult> UpdateAsync(UpdateGrapeSortRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<List<GrapeSortPhaseDatasetResult>> AddPhaseForecastModelDatasetsAsync(AddGrapeSortPhaseForecastModelDatasetsRequest request, List<FileDto> filesDtos, CancellationToken cancellationToken = default);
    Task<TrainModelResult> TrainModelByDatasetIdAsync(TrainPhaseModelRequest request, CancellationToken cancellationToken = default);
    Task AssignGrapeSortToPhaseAsync(AssignGrapeSortToPhaseRequest request, CancellationToken cancellationToken = default);
    Task<List<GrapeSortPhaseResult>> GetPhasesByGrapeSortIdAsync(string grapeSortId, CancellationToken cancellationToken = default);
}