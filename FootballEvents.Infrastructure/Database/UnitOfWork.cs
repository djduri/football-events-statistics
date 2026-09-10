using FootballEvents.Infrastructure.Abstractions;

namespace FootballEvents.Infrastructure.Database;

/// <summary>
/// Unit of work implementation for managing database transactions and persistence coordination.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;

    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    public ITransaction BeginTransaction()
    {
        return Transaction.Create(_databaseContext);
    }

    public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await Transaction.CreateAsync(_databaseContext);
    }

    public async Task<ITransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
    {
        return await Transaction.CreateAsync(_databaseContext, isolationLevel, cancellationToken);
    }

    public int SaveChanges()
    {
        return _databaseContext.SaveChanges();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _databaseContext.SaveChangesAsync(cancellationToken);
    }
}