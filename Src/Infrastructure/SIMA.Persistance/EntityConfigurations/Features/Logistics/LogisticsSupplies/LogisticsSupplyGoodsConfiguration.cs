using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyGoodsConfiguration : IEntityTypeConfiguration<LogisticsSupplyGoods>
{
    public void Configure(EntityTypeBuilder<LogisticsSupplyGoods> entity)
    {
        entity.ToTable("LogisticsSupplyGoods", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsSupplyGoodsId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.LogisticsRequestGoodsId)
            .HasConversion(x => x.Value, x => new LogisticsRequestGoodsId(x));
        entity.HasOne(x => x.LogisticsRequestGoods)
            .WithMany(x => x.LogisticsSupplyGoods)
            .HasForeignKey(x => x.LogisticsRequestGoodsId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsSupplyId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
        entity.HasOne(x => x.LogisticsSupply)
            .WithMany(x => x.LogisticsSupplyGoods)
            .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
