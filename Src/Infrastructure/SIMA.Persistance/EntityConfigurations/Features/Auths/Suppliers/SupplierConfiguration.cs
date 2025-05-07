using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.Suppliers
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.ToTable("Supplier", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.Code)
                        .HasMaxLength(20).IsUnicode();
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.IsInBlackList).HasMaxLength(1);

            entity.Property(x => x.SupplierRankId)
                .HasConversion(x => x.Value, x => new SupplierRankId(x));
            entity.HasOne(x => x.SupplierRank)
                .WithMany(x => x.Suppliers)
                .HasForeignKey(x => x.SupplierRankId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
