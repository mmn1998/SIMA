using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.ReceiveOrders;

public class ReceiveOrderConfiguration : IEntityTypeConfiguration<ReceiveOrder>
{
    public void Configure(EntityTypeBuilder<ReceiveOrder> entity)
    {
        entity.ToTable("ReceiveOrder", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ReceiveOrderId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ReceiptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ReceiptDocument)
            .WithMany(x => x.ReceiveOrders)
            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.OrderingId)
            .HasConversion(x => x.Value, x => new OrderingId(x));
        entity.HasOne(x => x.Ordering)
            .WithMany(x => x.ReceiveOrders)
            .HasForeignKey(x => x.OrderingId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
