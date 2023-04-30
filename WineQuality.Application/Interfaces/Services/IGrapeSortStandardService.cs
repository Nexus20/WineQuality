using WineQuality.Application.Models.Requests.GrapeSorts.Standards;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;

namespace WineQuality.Application.Interfaces.Services;

public interface IGrapeSortStandardService
{
    Task<GrapeSortProcessPhaseParameterStandardResult> CreateGrapeSortPhaseParameterStandardAsync(
        CreateGrapeSortProcessPhaseParameterStandardRequest request, CancellationToken cancellationToken = default);
}