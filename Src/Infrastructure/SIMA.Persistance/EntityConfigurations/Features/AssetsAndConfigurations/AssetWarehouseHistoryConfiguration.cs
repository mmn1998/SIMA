using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetWarehouseHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetWarehouseHistory> entity)
    {
        entity.ToTable("AssetWarehouseHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetWarehouseHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.IsCheckIn).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetWarehouseHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.WarehouseId)
          .HasConversion(
           v => v.Value,
           v => new WarehouseId(v));

        entity.HasOne(d => d.Warehouse)
            .WithMany(d => d.AssetWarehouseHistories)
            .HasForeignKey(d => d.WarehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
