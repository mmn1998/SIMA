using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.ReturnOrderingItems;

public class ReturnOrderingItemConfiguration : IEntityTypeConfiguration<ReturnOrderingItem>
{
    public void Configure(EntityTypeBuilder<ReturnOrderingItem> entity)
    {
        entity.ToTable("ReturnOrderingItem", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ReturnOrderingItemId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.OrderingItemId)
            .HasConversion(x => x.Value, x => new OrderingItemId(x));
        entity.HasOne(x => x.OrderingItem)
            .WithMany(x => x.ReturnOrderingItems)
            .HasForeignKey(x => x.OrderingItemId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ReciptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyDocumentId(x));
        entity.HasOne(x => x.ReciptDocument)
            .WithMany(x => x.ReturnOrderingItems)
            .HasForeignKey(x => x.ReciptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
