using FootballEvents.Application.Abstractions;

namespace FootballEvents.Application.Services.Interfaces;

internal interface IMatchLockService : ISingletonAppService
{
    Task<IDisposable> LockAsync(CancellationToken cancellationToken = default);
}