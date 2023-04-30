using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Application.Models.Results.ProcessPhaseParameterSensors;

namespace WineQuality.Application.Interfaces.Services;

public interface IProcessPhaseParameterSensorService
{
    Task<ProcessPhaseParameterSensorResult> CreateSensorAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default);
    Task<List<ProcessPhaseParameterSensorResult>> GetAsync(GetProcessPhaseParameterSensorsRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseParameterSensorResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}