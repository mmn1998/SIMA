using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsStatuses;

public class GoodsStatusConfiguration : IEntityTypeConfiguration<GoodsStatus>
{
    public void Configure(EntityTypeBuilder<GoodsStatus> entity)
    {
        entity.ToTable("GoodsStatus", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsStatusId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.IsRequiredItConfirmation).HasMaxLength(1).IsFixedLength();
        entity.Property(e => e.IsFinal).HasMaxLength(1).IsFixedLength();
    }
}
