using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.Orderings
{
    public class OrderingConfiguration : IEntityTypeConfiguration<Ordering>
    {
        public void Configure(EntityTypeBuilder<Ordering> entity)
        {
            entity.ToTable("Ordering", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new OrderingId(v))
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
                .WithMany(x => x.Orderings)
                .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.CandidatedSupplierId)
                .HasConversion(x => x.Value, x => new CandidatedSupplierId(x));
            entity.HasOne(x => x.LogisticsSupply)
                .WithMany(x => x.Orderings)
                .HasForeignKey(x => x.CandidatedSupplierId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.LogisticsSupplyId)
                .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
            entity.HasOne(x => x.LogisticsSupply)
                .WithMany(x => x.Orderings)
                .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
