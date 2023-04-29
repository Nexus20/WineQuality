using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;

namespace WineQuality.Application.Interfaces.Services;

public interface IProcessPhaseTypeService {
    Task<ProcessPhaseTypeResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<ProcessPhaseTypeResult>> GetAsync(GetProcessPhaseTypesRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseTypeResult> CreateAsync(CreateProcessPhaseTypeRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseTypeResult> UpdateAsync(UpdateProcessPhaseTypeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AddParameterAsync(AddParameterToPhaseRequest request, CancellationToken cancellationToken = default);
}