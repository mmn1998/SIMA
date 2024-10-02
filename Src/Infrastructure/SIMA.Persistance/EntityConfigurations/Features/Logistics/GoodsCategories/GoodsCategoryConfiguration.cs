using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsCategories;

public class GoodsCategoryConfiguration : IEntityTypeConfiguration<GoodsCategory>
{
    public void Configure(EntityTypeBuilder<GoodsCategory> entity)
    {
        entity.ToTable("GoodsCategory", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsCategoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code).HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique()
            .HasDatabaseName("Logistics.GoodsCategory.IX_GoodsCategory_Code1");
        entity.Property(x => x.IsGoods).HasMaxLength(1);
        entity.Property(x => x.IsHardware).HasMaxLength(1);
        entity.Property(x => x.IsRequiredSecurityCheck).HasMaxLength(1);
        entity.Property(x => x.IsTechnological).HasMaxLength(1);

        entity.Property(x => x.GoodsTypeId)
            .HasConversion(x => x.Value, x => new GoodsTypeId(x));
        entity.HasOne(x => x.GoodsType)
            .WithMany(x => x.GoodsCategories)
            .HasForeignKey(x => x.GoodsTypeId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}

