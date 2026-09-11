using FootballEvents.Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballEvents.Infrastructure.Database;

namespace FootballEvents.Infrastructure.Database.Configurations.Teams;
internal class TeamStatisticsConfiguration : IEntityTypeConfiguration<TeamStatistics>
{
    public void Configure(EntityTypeBuilder<TeamStatistics> builder)
    {
        builder.ToTable("TeamStatistics");

        builder.HasKey(x => x.TeamId);
    }
}