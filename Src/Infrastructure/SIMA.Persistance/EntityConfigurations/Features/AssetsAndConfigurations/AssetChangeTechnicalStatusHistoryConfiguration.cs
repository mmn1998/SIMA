using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetChangeTechnicalStatusHistoryConfiguration : IEntityTypeConfiguration<AssetChangeTechnicalStatusHistory>
{
    public void Configure(EntityTypeBuilder<AssetChangeTechnicalStatusHistory> entity)
    {
        entity.ToTable("AssetChangeTechnicalStatusHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetChangeTechnicalStatusHistoryId(v)).ValueGeneratedNever();
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
            .WithMany(d => d.AssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.FromAssetTechnicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetTechnicalStatusId(v));

        entity.HasOne(d => d.FromTechnicalStatus)
            .WithMany(d => d.FromAssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.FromAssetTechnicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ToAssetTechnicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetTechnicalStatusId(v));

        entity.HasOne(d => d.ToTechnicalStatus)
            .WithMany(d => d.ToAssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.ToAssetTechnicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
