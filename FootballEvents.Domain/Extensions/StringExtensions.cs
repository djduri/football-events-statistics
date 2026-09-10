using System.Globalization;
using System.Text;

namespace FootballEvents.Domain.Extensions;
public static class StringExtensions
{
    public static bool IsEmpty(this string @this) => string.IsNullOrEmpty(@this);

    public static string ToNormalizedKey(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString()
            .Normalize(NormalizationForm.FormC)
            .ToLowerInvariant()
            .Trim();
    }
}
