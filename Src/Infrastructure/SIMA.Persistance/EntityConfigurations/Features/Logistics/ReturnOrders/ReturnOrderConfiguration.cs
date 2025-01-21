using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.ReturnOrders
{
    //public class ReturnOrderConfiguration : IEntityTypeConfiguration<ReturnOrder>
    //{
    //    public void Configure(EntityTypeBuilder<ReturnOrder> entity)
    //    {
    //        entity.ToTable("ReturnOrder", "Logistics");
    //        entity.Property(x => x.Id)
    //    .HasConversion(
    //        v => v.Value,
    //        v => new ReturnOrderId(v))
    //    .ValueGeneratedNever();
    //        entity.HasKey(e => e.Id);
    //        entity.Property(e => e.CreatedAt)
    //                        .HasDefaultValueSql("(getdate())")
    //                        .HasColumnType("datetime");
    //        entity.Property(e => e.ModifiedAt)
    //                    .IsRowVersion()
    //                    .IsConcurrencyToken();

    //        entity.Property(x => x.ReceiptDocumentId)
    //            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
    //        entity.HasOne(x => x.ReceiptDocument)
    //            .WithMany(x => x.ReturnOrders)
    //            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

    //        entity.Property(x => x.LogisticsRequestId)
    //            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
    //        entity.HasOne(x => x.LogisticsRequest)
    //            .WithMany(x => x.ReturnOrders)
    //            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    //    }
    //}
}
