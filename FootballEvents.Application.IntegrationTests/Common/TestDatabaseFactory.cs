using FootballEvents.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FootballEvents.Application.IntegrationTests.Common;

/// <summary>
/// Factory responsible for creating isolated database contexts for integration tests.
/// </summary>
internal static class TestDatabaseFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="DatabaseContext"/> configured with a unique in-memory database.
    /// </summary>
    /// <returns>An isolated <see cref="DatabaseContext"/> instance.</returns>
    public static DatabaseContext CreateInMemoryContext()
    {
        // Configure EF Core to use an in-memory database with a unique name per call to ensure test isolation
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Return a new context instance with the generated options
        return new DatabaseContext(options);
    }
}