using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhases;

namespace WineQuality.Application.Interfaces.Services;

public interface IProcessPhaseService {
    Task<ProcessPhaseResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<ProcessPhaseResult>> GetAsync(GetProcessPhasesRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseResult> CreateAsync(CreateProcessPhaseRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseResult> UpdateAsync(UpdateProcessPhaseRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AddParametersAsync(AddParametersToPhaseRequest request, CancellationToken cancellationToken = default);
    Task RemoveParametersAsync(RemoveParametersFromPhaseRequest request, CancellationToken cancellationToken = default);
}