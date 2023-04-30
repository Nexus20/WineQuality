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
    public IRepository<WineMaterialBatchGrapeSortPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; }
    public IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> WineMaterialBatchProcessParameterValues { get; }
    public IRepository<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; }
    public IRepository<GrapeSortPhaseDataset> GrapeSortPhaseDatasets { get; }

    public UnitOfWork(ApplicationDbContext dbContext, IRepository<ProcessParameter> processParameters,
        IRepository<ProcessPhase> processPhases, IRepository<ProcessPhaseParameter> processPhaseParameters,
        IRepository<WineMaterialBatch> wineMaterialBatches,
        IRepository<WineMaterialBatchGrapeSortPhase> wineMaterialBatchProcessPhases,
        IRepository<WineMaterialBatchGrapeSortPhaseParameter> wineMaterialBatchProcessPhaseParameters,
        IRepository<WineMaterialBatchGrapeSortPhaseParameterValue> wineMaterialBatchProcessParameterValues,
        IRepository<GrapeSort> grapeSorts, IRepository<GrapeSortPhaseForecastModel> grapeSortPhaseForecastModels, IRepository<GrapeSortPhaseDataset> grapeSortPhaseDatasets, IRepository<GrapeSortPhase> grapeSortPhases, IRepository<GrapeSortProcessPhaseParameterStandard> grapeSortProcessPhaseParameterStandards)
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