using FootballEvents.Domain.Teams;
using Microsoft.EntityFrameworkCore;
using FootballEvents.Domain.Base;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FootballEvents.Application.IntegrationTests")]

namespace FootballEvents.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    private bool _updateAuditableSubjectProperties = true;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected DatabaseContext()
    {
    }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamStatistics> TeamStatistics => Set<TeamStatistics>();
    public DbSet<MatchRecord> MatchRecords => Set<MatchRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);
    }

    internal void ChangeAuditableSubjectUpdateBehaviour(bool updateAuditableSubjectProperties)
    {
        _updateAuditableSubjectProperties = updateAuditableSubjectProperties;
    }

    public override int SaveChanges()
    {
        HandleAuditableEntityChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        HandleAuditableEntityChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleAuditableEntityChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        HandleAuditableEntityChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void HandleAuditableEntityChanges()
    {
        if (!_updateAuditableSubjectProperties)
            return;

        var entities = ChangeTracker
            .Entries()
            .Where(entry => entry.Entity is IAuditableEntity &&
                (entry.State == EntityState.Added || entry.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            ((IAuditableEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((IAuditableEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
            }
        }
    }
}