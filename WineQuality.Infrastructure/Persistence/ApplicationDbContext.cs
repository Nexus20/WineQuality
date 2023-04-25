using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WineQuality.Domain.Entities;
using WineQuality.Domain.Entities.Abstract;
using WineQuality.Infrastructure.Identity;

namespace WineQuality.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public DbSet<GrapeSort> GrapeSorts { get; set; }
    public DbSet<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; set; }
    public DbSet<ProcessParameter> ProcessParameters { get; set; }
    public DbSet<ProcessPhaseType> ProcessPhaseTypes { get; set; }
    public DbSet<ProcessPhaseParameter> ProcessPhaseParameters { get; set; }
    public DbSet<WineMaterialBatch> WineMaterialBatches { get; set; }
    public DbSet<WineMaterialBatchProcessPhase> WineMaterialBatchProcessPhases { get; set; }
    public DbSet<WineMaterialBatchProcessPhaseParameter> WineMaterialBatchProcessPhaseParameters { get; set; }
    public DbSet<WineMaterialBatchProcessParameterValue> WineMaterialBatchProcessParameterValues { get; set; }
    public DbSet<FileReference> FileReferences { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        if (!Database.IsInMemory())
        {
            Database.Migrate();
        }
    }

    public override int SaveChanges()
    {
        AddInfoBeforeUpdate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        AddInfoBeforeUpdate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddInfoBeforeUpdate()
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.Entity is ITimeMarkedEntity && x.State is EntityState.Added or EntityState.Modified);
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((ITimeMarkedEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            ((ITimeMarkedEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
    }
}