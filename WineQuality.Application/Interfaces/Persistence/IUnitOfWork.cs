using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IRepository<GrapeSort> GrapeSorts { get; }
    IRepository<GrapeSortPhase> GrapeSortPhases { get; }
    IRepository<GrapeSortProcessPhaseParameterStandard> GrapeSortProcessPhaseParameterStandards { get; }
    IRepository<ProcessParameter> ProcessParameters { get; }
    IRepository<ProcessPhase> ProcessPhases { get; }
    IRepository<ProcessPhaseParameter> ProcessPhaseParameters { get; }
    IRepository<WineMaterialBatch> WineMaterialBatches { get; }
    IRepository<WineMaterialBatchGrapeSortPhase> WineMaterialBatchGrapeSortPhases { get; }
    IRepository<WineMaterialBatchGrapeSortPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; }
    IRepository<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; }
    IRepository<GrapeSortPhaseDataset> GrapeSortPhaseDatasets { get; }
    IRepository<ProcessPhaseParameterSensor> ProcessPhaseParameterSensors { get; }
    IRepository<WineMaterialBatchGrapeSortPhaseParameter> WineMaterialBatchGrapeSortPhaseParameters { get; }
    IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> WineMaterialBatchGrapeSortPhaseParameterValues { get; }
    IRepository<FileReference> FileReferences { get; }
    IRepository<QualityPrediction> QualityPredictions { get; }
    IRepository<Culture> Cultures { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}