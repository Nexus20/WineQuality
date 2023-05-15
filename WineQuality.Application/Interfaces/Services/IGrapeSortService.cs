using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Interfaces.Services;

public interface IGrapeSortService {
    Task<GrapeSortDetailsResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<GrapeSortResult>> GetAsync(GetGrapeSortsRequest request, CancellationToken cancellationToken = default);
    Task<GrapeSortResult> CreateAsync(CreateGrapeSortRequest request, CancellationToken cancellationToken = default);
    Task<GrapeSortResult> UpdateAsync(UpdateGrapeSortRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AssignGrapeSortToPhaseAsync(AssignGrapeSortToPhaseRequest request, CancellationToken cancellationToken = default);
    Task<List<GrapeSortPhaseResult>> GetPhasesByGrapeSortIdAsync(string grapeSortId, CancellationToken cancellationToken = default);
    Task SaveGrapeSortPhasesOrderAsync(SaveGrapeSortPhasesOrderRequest request, CancellationToken cancellationToken = default);
}