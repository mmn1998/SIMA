using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.DeliveryItemConfiguration;

public class DeliveryItemConfiguration : IEntityTypeConfiguration<DeliveryItem>
{
    public void Configure(EntityTypeBuilder<DeliveryItem> entity)
    {
        entity.ToTable("DeliveryItem", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new DeliveryItemId(v))
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
            .WithMany(x => x.DeliveryItems)
            .HasForeignKey(x => x.OrderingItemId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ReciptDocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x => x.ReciptDocument)
            .WithMany(x => x.DeliveryItems)
            .HasForeignKey(x => x.ReciptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
