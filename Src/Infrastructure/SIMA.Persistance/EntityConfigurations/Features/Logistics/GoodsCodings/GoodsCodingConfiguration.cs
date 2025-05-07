using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsCodings;

public class GoodsCodingConfiguration : IEntityTypeConfiguration<GoodsCoding>
{
    public void Configure(EntityTypeBuilder<GoodsCoding> entity)
    {
        entity.ToTable("GoodsCoding", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsCodingId(v))
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

        entity.Property(x => x.LogisticsSupplyGoodsId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyGoodsId(x));
        entity.HasOne(x => x.LogisticsSupplyGoods)
            .WithMany(x => x.GoodsCodings)
            .HasForeignKey(x => x.LogisticsSupplyGoodsId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
