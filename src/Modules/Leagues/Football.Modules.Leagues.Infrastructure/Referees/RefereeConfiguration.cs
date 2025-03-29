using Football.Modules.Leagues.Domain.Referees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Modules.Leagues.Infrastructure.Referees;

public class RefereeConfiguration : IEntityTypeConfiguration<Referee>
{
    public void Configure(EntityTypeBuilder<Referee> builder)
    {
        builder.ToTable(nameof(Referee));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.MinutesPlayed).HasDefaultValue(0);
    }
}