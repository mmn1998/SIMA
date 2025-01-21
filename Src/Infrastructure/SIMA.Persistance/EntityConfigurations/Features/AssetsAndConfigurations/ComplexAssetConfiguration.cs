using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations
{
    public class ComplexAssetConfiguration : IEntityTypeConfiguration<ComplexAsset>
    {
        public void Configure(EntityTypeBuilder<ComplexAsset> entity)
        {
            entity.ToTable("ComplexAsset", "AssetAndConfiguration");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ComplexAssetId(v)).ValueGeneratedNever();
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
                .WithMany(d => d.Assets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.ParentAssetId)
               .HasConversion(
                v => v.Value,
                v => new AssetId(v));

            entity.HasOne(d => d.ParentAsset)
                .WithMany(d => d.ParentAssets)
                .HasForeignKey(d => d.ParentAssetId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
