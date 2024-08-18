using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemAssetConfiguration : IEntityTypeConfiguration<ConfigurationItemAsset>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemAsset> entity)
    {
        entity.ToTable("ConfigurationItemAsset", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemAssetId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemAssets)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.ConfigurationItemAssets)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
