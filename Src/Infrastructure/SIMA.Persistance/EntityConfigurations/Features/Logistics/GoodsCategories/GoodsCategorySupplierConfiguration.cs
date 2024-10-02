using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsCategories;

public class GoodsCategorySupplierConfiguration : IEntityTypeConfiguration<GoodsCategorySupplier>
{
    public void Configure(EntityTypeBuilder<GoodsCategorySupplier> entity)
    {
        entity.ToTable("GoodsCategorySupplier", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsCategorySupplierId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.GoodsCategoryId)
            .HasConversion(x => x.Value, x => new GoodsCategoryId(x));
        entity.HasOne(x => x.GoodsCategory)
            .WithMany(x => x.GoodsCategorySuppliers)
            .HasForeignKey(x => x.GoodsCategoryId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SupplierId)
            .HasConversion(x => x.Value, x => new SupplierId(x));
        entity.HasOne(x => x.Supplier)
            .WithMany(x => x.GoodsCategorySuppliers)
            .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}


