using FootballEvents.Domain.Common;

namespace FootballEvents.Application.Common;
public sealed record SortingArguments(string SortBy, bool Desc);