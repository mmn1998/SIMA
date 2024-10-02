using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class PositionLevelConfiguration : IEntityTypeConfiguration<PositionLevel>
{
    public void Configure(EntityTypeBuilder<PositionLevel> entity)
    {
        entity.ToTable("PositionLevel", "Organization");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new PositionLevelId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(x => x.IsList).HasMaxLength(1).IsFixedLength();
    }
}
