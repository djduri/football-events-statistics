using FootballEvents.Domain.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballEvents.Infrastructure.Database;

namespace FootballEvents.Infrastructure.Database.Configurations.Teams;
internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams", Schema.Teams);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name).IsUnique();     

        builder.Property(x => x.Name).HasMaxLength(255);
       
        builder.HasOne(x => x.Statistics)
               .WithOne(x => x.Team)
               .HasForeignKey<TeamStatistics>(x => x.TeamId) 
               .OnDelete(DeleteBehavior.Cascade) 
               .IsRequired();
    }
}