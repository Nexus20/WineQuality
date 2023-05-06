using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.Services;

public interface IMachineLearningService
{
    Task<TrainModelResult> TrainPhaseModelAsync(GrapeSortPhaseDataset dataset, CancellationToken cancellationToken = default);
    Task<PredictionResult> PredictQualityAsync(GrapeSortPhaseForecastModel forecastModelEntity,
        Dictionary<string, double> requestParametersValues,
        CancellationToken cancellationToken = default);
}