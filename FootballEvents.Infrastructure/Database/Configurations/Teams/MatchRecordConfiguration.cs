using FootballEvents.Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballEvents.Infrastructure.Database;

namespace FootballEvents.Infrastructure.Database.Configurations.Teams;
internal class MatchRecordConfiguration : IEntityTypeConfiguration<MatchRecord>
{
    public void Configure(EntityTypeBuilder<MatchRecord> builder)
    {
        builder.ToTable("MatchRecords", Schema.Teams);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.HomeTeam)
               .WithMany() 
               .HasForeignKey(x => x.HomeTeamId)
               .OnDelete(DeleteBehavior.Restrict) 
               .IsRequired();

        builder.HasOne(x => x.AwayTeam)
               .WithMany()
               .HasForeignKey(x => x.AwayTeamId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
    }
}