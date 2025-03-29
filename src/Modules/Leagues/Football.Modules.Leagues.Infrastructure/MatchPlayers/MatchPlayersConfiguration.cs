using Football.Modules.Leagues.Domain.MatchPlayers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Modules.Leagues.Infrastructure.MatchPlayers;

internal sealed class MatchPlayersConfiguration : IEntityTypeConfiguration<MatchPlayer>
{
    public void Configure(EntityTypeBuilder<MatchPlayer> builder)
    {
        builder.ToTable(nameof(MatchPlayer));
        builder.HasKey(x => new { x.MatchId, x.PlayerId });
        builder.Property(x => x.PlayerType).HasConversion<string>().IsRequired();
        
        builder.HasKey(x => x.PlayerType);
        builder.HasKey(x => x.MatchId);
        builder.HasKey(x => x.PlayerId);
        
        builder.HasOne(x => x.Player)
            .WithMany(y => y.MatchPlayers)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Match)
            .WithMany(y => y.MatchPlayers)
            .HasForeignKey(x => x.MatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}