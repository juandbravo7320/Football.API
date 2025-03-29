using Football.Modules.Leagues.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Modules.Leagues.Infrastructure.Players;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(nameof(Player));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.YellowCard).HasDefaultValue(false);
        builder.Property(x => x.RedCard).HasDefaultValue(false);
        builder.Property(x => x.MinutesPlayed).HasDefaultValue(0);
    }
}