using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetChangePhysicalStatusHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetChangePhysicalStatusHistory> entity)
    {
        entity.ToTable("AssetChangePhysicalStatusHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetChangePhysicalStatusHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.FromAssetPhysicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetPhysicalStatusId(v));

        entity.HasOne(d => d.FromAssetPhysicalStatus)
            .WithMany(d => d.FromAssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.FromAssetPhysicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ToAssetPhysicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetPhysicalStatusId(v));

        entity.HasOne(d => d.ToAssetPhysicalStatus)
            .WithMany(d => d.ToAssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.ToAssetPhysicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetTechnicalStatusConfiguration
{
    public void Configure(EntityTypeBuilder<AssetTechnicalStatus> entity)
    {
        entity.ToTable("AssetTechnicalStatusConfiguration", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetTechnicalStatusId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.HasIndex(i => i.Code).IsUnique();
        entity.Property(x => x.Code)
          .HasMaxLength(20);
        entity.Property(x => x.Name)
          .HasMaxLength(200);
    }
}
