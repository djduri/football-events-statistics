using FootballEvents.Domain.Teams;
using Microsoft.EntityFrameworkCore;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FootballEvents.Application.IntegrationTests")]

namespace FootballEvents.Infrastructure.Database;

/// <summary>
/// Entity Framework Core database context for managing application persistence.
/// </summary>
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected DatabaseContext()
    {
    }

    // DbSets representing core domain aggregates and entities
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamStatistics> TeamStatistics => Set<TeamStatistics>();
    public DbSet<MatchRecord> MatchRecords => Set<MatchRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply entity configurations from the application assembly
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);
    }
}