using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.Goodses
{
    public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> entity)
        {
            entity.ToTable("Goods", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new GoodsId(v))
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
            
            entity.Property(x => x.UnitMeasurementId)
                .HasConversion(x => x.Value, x => new UnitMeasurementId(x));
            entity.HasOne(x => x.UnitMeasurement)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.UnitMeasurementId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.GoodsCategoryId)
                .HasConversion(x => x.Value, x => new GoodsCategoryId(x));
            entity.HasOne(x => x.GoodsCategory)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.GoodsCategoryId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
