using FootballEvents.Domain.Common;

namespace FootballEvents.Infrastructure.Abstractions.Settings;

public sealed class DatabaseSettings : IValidatableSettings
{
    public string? ConnectionString { get; init; }
    public bool AutoMigrate { get; init; }

    public bool Valid()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString)) return false;

        return true;
    }
}
