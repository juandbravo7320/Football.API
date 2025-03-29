using Football.Modules.Leagues.Domain.Matches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Modules.Leagues.Infrastructure.Matches;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable(nameof(Match));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StartsAtUtc).IsRequired();
        
        builder.HasOne(x => x.HouseManager)
            .WithMany()
            .HasForeignKey(x => x.HouseManagerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.AwayManager)
            .WithMany()
            .HasForeignKey(x => x.AwayManagerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(x => x.Referee)
            .WithMany()
            .HasForeignKey(x => x.RefereeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}