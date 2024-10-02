using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierBlackListHistories
{
    public class SupplierBlackListHistoryConfiguration : IEntityTypeConfiguration<SupplierBlackListHistory>
    {
        public void Configure(EntityTypeBuilder<SupplierBlackListHistory> entity)
        {
            entity.ToTable("SupplierBlackListHistory", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierBlackListHistoryId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierBlackListHistories)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.OrderingId)
                .HasConversion(x => x.Value, x => new OrderingId(x));
            entity.HasOne(x => x.Ordering)
                .WithMany(x => x.SupplierBlackListHistories)
                .HasForeignKey(x => x.OrderingId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
