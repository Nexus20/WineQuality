using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    IRepository<GrapeSort> GrapeSorts { get; }
    IRepository<ProcessParameter> ProcessParameters { get; }
    IRepository<ProcessPhaseType> ProcessPhaseTypes { get; }
    IRepository<ProcessPhaseParameter> ProcessPhaseParameters { get; }
    IRepository<WineMaterialBatch> WineMaterialBatches { get; }
    IRepository<WineMaterialBatchProcessPhase> WineMaterialBatchProcessPhases { get; }
    IRepository<WineMaterialBatchProcessPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; }
    IRepository<WineMaterialBatchProcessParameterValue> WineMaterialBatchProcessParameterValues { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}