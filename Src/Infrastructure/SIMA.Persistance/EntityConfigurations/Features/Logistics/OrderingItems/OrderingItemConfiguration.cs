using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.OrderingItems;

public class OrderingItemConfiguration : IEntityTypeConfiguration<OrderingItem>
{
    public void Configure(EntityTypeBuilder<OrderingItem> entity)
    {
        entity.ToTable("OrderingItem", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new OrderingItemId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.OrderingId)
            .HasConversion(x => x.Value, x => new OrderingId(x));
        entity.HasOne(x => x.Ordering)
            .WithMany(x => x.OrderingItems)
            .HasForeignKey(x => x.OrderingId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsSupplyGoodsId)
         .HasConversion(x => x.Value, x => new LogisticsSupplyGoodsId(x));
        entity.HasOne(x => x.LogisticsSupplyGoods)
            .WithMany(x => x.OrderingItems)
            .HasForeignKey(x => x.LogisticsSupplyGoodsId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
