﻿using WineQuality.Application.Models.Requests.GrapeSorts.Standards;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;

namespace WineQuality.Application.Interfaces.Services;

public interface IGrapeSortStandardService
{
    Task<GrapeSortProcessPhaseParameterStandardResult> CreateGrapeSortPhaseParameterStandardAsync(
        CreateGrapeSortProcessPhaseParameterStandardRequest request, CancellationToken cancellationToken = default);

    Task UpdateGrapeSortPhaseParameterStandardsAsync(UpdateGrapeSortProcessPhaseParameterStandardsRequest request, CancellationToken cancellationToken = default);
    Task<GrapeSortProcessPhaseParameterStandardResult> GetGrapeSortPhaseParameterStandardByIdAsync(string standardId, CancellationToken cancellationToken = default);
    Task UpdateGrapeSortPhaseParameterStandardAsync(UpdateGrapeSortProcessPhaseParameterStandardsRequestPart request, CancellationToken cancellationToken = default);
    Task DeleteGrapeSortPhaseParameterStandardByIdAsync(string standardId, CancellationToken cancellationToken = default);
}