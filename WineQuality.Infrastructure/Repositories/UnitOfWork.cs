using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Domain.Entities;
using WineQuality.Infrastructure.Persistence;

namespace WineQuality.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IRepository<GrapeSort> GrapeSorts { get; }
    public IRepository<ProcessParameter> ProcessParameters { get; }
    public IRepository<ProcessPhaseType> ProcessPhaseTypes { get; }
    public IRepository<ProcessPhaseParameter> ProcessPhaseParameters { get; }
    public IRepository<WineMaterialBatch> WineMaterialBatches { get; }
    public IRepository<WineMaterialBatchProcessPhase> WineMaterialBatchProcessPhases { get; }
    public IRepository<WineMaterialBatchProcessPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; }
    public IRepository<WineMaterialBatchProcessParameterValue> WineMaterialBatchProcessParameterValues { get; }

    public UnitOfWork(ApplicationDbContext dbContext, IRepository<ProcessParameter> processParameters,
        IRepository<ProcessPhaseType> processPhaseTypes, IRepository<ProcessPhaseParameter> processPhaseParameters,
        IRepository<WineMaterialBatch> wineMaterialBatches,
        IRepository<WineMaterialBatchProcessPhase> wineMaterialBatchProcessPhases,
        IRepository<WineMaterialBatchProcessPhaseParameter> wineMaterialBatchProcessPhaseParameters,
        IRepository<WineMaterialBatchProcessParameterValue> wineMaterialBatchProcessParameterValues,
        IRepository<GrapeSort> grapeSorts)
    {
        _dbContext = dbContext;
        ProcessParameters = processParameters;
        ProcessPhaseTypes = processPhaseTypes;
        ProcessPhaseParameters = processPhaseParameters;
        WineMaterialBatches = wineMaterialBatches;
        WineMaterialBatchProcessPhases = wineMaterialBatchProcessPhases;
        WineMaterialBatchProcessPhaseParameters = wineMaterialBatchProcessPhaseParameters;
        WineMaterialBatchProcessParameterValues = wineMaterialBatchProcessParameterValues;
        GrapeSorts = grapeSorts;
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