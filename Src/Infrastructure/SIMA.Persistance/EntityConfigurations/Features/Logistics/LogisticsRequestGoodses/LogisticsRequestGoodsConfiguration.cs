using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsRequestGoodses;

public class LogisticsRequestGoodsConfiguration : IEntityTypeConfiguration<Domain.Models.Features.Logistics.LogisticsRequests.Entities.LogisticsRequestGoods>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Features.Logistics.LogisticsRequests.Entities.LogisticsRequestGoods> entity)
    {
        entity.ToTable("LogisticsRequestGoods", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsRequestGoodsId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.GoodsId)
            .HasConversion(x => x.Value, x => new GoodsId(x));
        entity.HasOne(x => x.Goods)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.GoodsId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.GoodsCategoryId)
            .HasConversion(x => x.Value, x => new GoodsCategoryId(x));
        entity.HasOne(x => x.GoodsCategory)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.GoodsCategoryId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.GoodsStatusId)
            .HasConversion(x => x.Value, x => new GoodsStatusId(x));
        entity.HasOne(x => x.GoodsStatus)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.GoodsStatusId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
