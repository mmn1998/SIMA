using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class PositionTypeConfiguration : IEntityTypeConfiguration<PositionType>
{
    public void Configure(EntityTypeBuilder<PositionType> entity)
    {
        entity.ToTable("PositionType", "Organization");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new PositionTypeId(v)).ValueGeneratedNever();
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
