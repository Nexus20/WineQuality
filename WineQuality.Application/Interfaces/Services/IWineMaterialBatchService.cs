using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.WineMaterialBatches;

namespace WineQuality.Application.Interfaces.Services;

public interface IWineMaterialBatchService {
    Task<WineMaterialBatchResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<WineMaterialBatchResult>> GetAsync(GetWineMaterialBatchesRequest request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchResult> CreateAsync(CreateWineMaterialBatchRequest request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchResult> UpdateAsync(UpdateWineMaterialBatchRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AssignWineMaterialBatchToPhaseAsync(AssignWineMaterialBatchToPhaseRequest request, CancellationToken cancellationToken = default);
}