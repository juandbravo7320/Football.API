using Football.Modules.Leagues.Domain.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football.Modules.Leagues.Infrastructure.Managers;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable(nameof(Manager));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.YellowCard).HasDefaultValue(false);
        builder.Property(x => x.RedCard).HasDefaultValue(false);
    }
}