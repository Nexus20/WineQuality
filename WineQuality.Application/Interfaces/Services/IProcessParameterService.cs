using WineQuality.Application.Models.Requests.ProcessParameters;
using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Interfaces.Services;

public interface IProcessParameterService {
    Task<ProcessParameterDetailsResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<ProcessParameterResult>> GetAsync(GetProcessParametersRequest request, CancellationToken cancellationToken = default);
    Task<ProcessParameterResult> CreateAsync(CreateProcessParameterRequest request, CancellationToken cancellationToken = default);
    Task<ProcessParameterResult> UpdateAsync(UpdateProcessParameterRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}