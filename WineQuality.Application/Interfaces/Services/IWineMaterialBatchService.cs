using WineQuality.Application.Models.Requests.WineMaterialBatches;
using WineQuality.Application.Models.Results.WineMaterialBatches;

namespace WineQuality.Application.Interfaces.Services;

public interface IWineMaterialBatchService {
    Task<WineMaterialBatchResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<WineMaterialBatchResult>> GetAsync(GetWineMaterialBatchesRequest request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchResult> CreateAsync(CreateWineMaterialBatchRequest request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchResult> UpdateAsync(UpdateWineMaterialBatchRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AssignWineMaterialBatchToPhasesAsync(AssignWineMaterialBatchToPhasesRequest request, CancellationToken cancellationToken = default);
    Task RunWineProcessingForPhaseAsync(ChangeWineProcessingRequestRunningState request, CancellationToken cancellationToken = default);
    Task StopWineProcessingForPhaseAsync(ChangeWineProcessingRequestRunningState request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchDetailsResult> GetDetailsByIdAsync(string id, CancellationToken cancellationToken = default);
    Task UpdatePhasesTermsAsync(UpdateWineMaterialBatchPhasesTermsRequest request, CancellationToken cancellationToken = default);
    Task<WineMaterialBatchProcessStartAllowedResult> CheckIfProcessStartAllowedAsync(string id, CancellationToken cancellationToken = default);
}