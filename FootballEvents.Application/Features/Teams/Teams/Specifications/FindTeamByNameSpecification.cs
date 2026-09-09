using FootballEvents.Domain.Extensions;
using FootballEvents.Domain.Teams;
using FootballEvents.Infrastructure.Abstractions;

namespace FootballEvents.Application.Features.Teams.Teams.Specifications;
internal sealed class FindTeamByNameSpecification : Specification<Team>
{
    public FindTeamByNameSpecification(string name)
        : base(x => x.NormalizedName == name.ToNormalizedKey())
    {
        AddInclude(x => x.Statistics);
    }
}
