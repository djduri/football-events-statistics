using FootballEvents.Domain.Common;

namespace FootballEvents.Application.Abstractions.Settings;
public sealed class ApiUrlSettings : IValidatableSettings
{
    public string? Url { get; init; }
    public string? NfcLink { get; init; }

    public bool Valid()
    {
        if (string.IsNullOrWhiteSpace(Url)) return false;

        if (string.IsNullOrWhiteSpace(NfcLink)) return false;

        return true;
    }
}
