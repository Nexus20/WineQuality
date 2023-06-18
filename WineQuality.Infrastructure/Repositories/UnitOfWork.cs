using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Domain.Entities;
using WineQuality.Infrastructure.Persistence;

namespace WineQuality.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IRepository<GrapeSort> GrapeSorts { get; }
    public IRepository<GrapeSortPhase> GrapeSortPhases { get; }
    public IRepository<GrapeSortProcessPhaseParameterStandard> GrapeSortProcessPhaseParameterStandards { get; }
    public IRepository<ProcessParameter> ProcessParameters { get; }
    public IRepository<ProcessPhase> ProcessPhases { get; }
    public IRepository<ProcessPhaseParameter> ProcessPhaseParameters { get; }
    public IRepository<WineMaterialBatch> WineMaterialBatches { get; }
    public IRepository<WineMaterialBatchGrapeSortPhase> WineMaterialBatchProcessPhases { get; }
    public IRepository<WineMaterialBatchGrapeSortPhase> WineMaterialBatchGrapeSortPhases { get; }
    public IRepository<WineMaterialBatchGrapeSortPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; }
    public IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> WineMaterialBatchProcessParameterValues { get; }
    public IRepository<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; }
    public IRepository<GrapeSortPhaseDataset> GrapeSortPhaseDatasets { get; }
    public IRepository<ProcessPhaseParameterSensor> ProcessPhaseParameterSensors { get; }
    public IRepository<WineMaterialBatchGrapeSortPhaseParameter> WineMaterialBatchGrapeSortPhaseParameters { get; }
    public IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> WineMaterialBatchGrapeSortPhaseParameterValues
    {
        get;
    }

    public IRepository<FileReference> FileReferences { get; }
    public IRepository<QualityPrediction> QualityPredictions { get; }
    public IRepository<Culture> Cultures { get; set; }

    public UnitOfWork(ApplicationDbContext dbContext, IRepository<ProcessParameter> processParameters,
        IRepository<ProcessPhase> processPhases, IRepository<ProcessPhaseParameter> processPhaseParameters,
        IRepository<WineMaterialBatch> wineMaterialBatches,
        IRepository<WineMaterialBatchGrapeSortPhase> wineMaterialBatchProcessPhases,
        IRepository<WineMaterialBatchGrapeSortPhaseParameter> wineMaterialBatchProcessPhaseParameters,
        IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> wineMaterialBatchProcessParameterValues,
        IRepository<GrapeSort> grapeSorts, IRepository<GrapeSortPhaseForecastModel> grapeSortPhaseForecastModels,
        IRepository<GrapeSortPhaseDataset> grapeSortPhaseDatasets, IRepository<GrapeSortPhase> grapeSortPhases,
        IRepository<GrapeSortProcessPhaseParameterStandard> grapeSortProcessPhaseParameterStandards,
        IRepository<ProcessPhaseParameterSensor> processPhaseParameterSensors,
        IRepository<WineMaterialBatchGrapeSortPhaseParameter> wineMaterialBatchGrapeSortPhaseParameters, 
        IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> wineMaterialBatchGrapeSortPhaseParameterValues, 
        IRepository<WineMaterialBatchGrapeSortPhase> wineMaterialBatchGrapeSortPhases, 
        IRepository<FileReference> fileReferences, 
        IRepository<QualityPrediction> qualityPredictions, IRepository<Culture> cultures)
    {
        _dbContext = dbContext;
        ProcessParameters = processParameters;
        ProcessPhases = processPhases;
        ProcessPhaseParameters = processPhaseParameters;
        WineMaterialBatches = wineMaterialBatches;
        WineMaterialBatchProcessPhases = wineMaterialBatchProcessPhases;
        WineMaterialBatchProcessPhaseParameters = wineMaterialBatchProcessPhaseParameters;
        WineMaterialBatchProcessParameterValues = wineMaterialBatchProcessParameterValues;
        GrapeSorts = grapeSorts;
        GrapeSortPhaseForecastModels = grapeSortPhaseForecastModels;
        GrapeSortPhaseDatasets = grapeSortPhaseDatasets;
        GrapeSortPhases = grapeSortPhases;
        GrapeSortProcessPhaseParameterStandards = grapeSortProcessPhaseParameterStandards;
        ProcessPhaseParameterSensors = processPhaseParameterSensors;
        WineMaterialBatchGrapeSortPhaseParameters = wineMaterialBatchGrapeSortPhaseParameters;
        WineMaterialBatchGrapeSortPhaseParameterValues = wineMaterialBatchGrapeSortPhaseParameterValues;
        WineMaterialBatchGrapeSortPhases = wineMaterialBatchGrapeSortPhases;
        FileReferences = fileReferences;
        QualityPredictions = qualityPredictions;
        Cultures = cultures;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}