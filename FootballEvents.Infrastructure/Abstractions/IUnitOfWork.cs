using System.Linq.Expressions;
using FootballEvents.Domain.Base;

namespace FootballEvents.Infrastructure.Abstractions;

public interface IUnitOfWork
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    ITransaction BeginTransaction();
    Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<ITransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel,
                                             CancellationToken cancellationToken = default);
}
