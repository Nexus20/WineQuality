using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Interfaces.Services;

public interface IModelTrainingService
{
    Task<TrainModelResult> TrainPhaseModelAsync(string datasetId, CancellationToken cancellationToken = default);
}