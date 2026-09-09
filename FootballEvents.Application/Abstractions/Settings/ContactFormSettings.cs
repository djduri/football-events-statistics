using FootballEvents.Domain.Common;

namespace FootballEvents.Application.Abstractions.Settings;

public class ContactFormSettings : IValidatableSettings
{
    public string? RecipientEmail { get; init; }
    public string? SubjectPrefix { get; init; }
    public bool SaveToDatabase { get; init; }

    public bool Valid()
    {
        if (string.IsNullOrWhiteSpace(RecipientEmail)) return false;

        if (string.IsNullOrWhiteSpace(SubjectPrefix)) return false;

        return true;
    }
}
