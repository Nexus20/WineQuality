﻿using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Interfaces.Services;

public interface IQualityPredictionService
{
    Task<GrapeSortPhaseForecastModelResult> TrainModelByDatasetIdAsync(TrainPhaseModelRequest request, CancellationToken cancellationToken = default);

    Task<List<GrapeSortPhaseForecastModelResult>> GetGrapeSortPhaseForecastModelsAsync(string grapeSortPhaseId,
        CancellationToken cancellationToken = default);

    Task<PredictionDetailsResult> PredictQualityAsync(PredictQualityRequest request, CancellationToken cancellationToken = default);
    Task DeleteGrapeSortPhaseForecastModelAsync(string id, CancellationToken cancellationToken = default);
    Task<List<PredictionDetailsResult>> GetGrapeSortPhasePredictionsHistoryAsync(string grapeSortPhaseId, CancellationToken cancellationToken);
    Task<PredictionDetailsResult> GetPredictionDetailsAsync(string id, CancellationToken cancellationToken);
}