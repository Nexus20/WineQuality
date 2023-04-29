using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.Services;

public interface IModelTrainingService
{
    Task<TrainModelResult> TrainPhaseModelAsync(GrapeSortPhaseDataset dataset, CancellationToken cancellationToken = default);
}