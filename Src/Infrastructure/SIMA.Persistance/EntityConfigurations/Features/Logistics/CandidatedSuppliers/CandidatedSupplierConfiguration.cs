using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.CandidatedSuppliers
{
    public class CandidatedSupplierConfiguration : IEntityTypeConfiguration<CandidatedSupplier>
    {
        public void Configure(EntityTypeBuilder<CandidatedSupplier> entity)
        {
            entity.ToTable("CandidatedSupplier", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new CandidatedSupplierId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.IsSelected).HasMaxLength(1).IsFixedLength();

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.CandidatedSuppliers)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.LogisticsSupplyId)
                .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
            entity.HasOne(x => x.LogisticsSupply)
                .WithMany(x => x.CandidatedSuppliers)
                .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
