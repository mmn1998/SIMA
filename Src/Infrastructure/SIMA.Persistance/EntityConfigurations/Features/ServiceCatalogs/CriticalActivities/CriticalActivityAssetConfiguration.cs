using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.CriticalActivities;

public class CriticalActivityAssetConfiguration : IEntityTypeConfiguration<CriticalActivityAsset>
{
    public void Configure(EntityTypeBuilder<CriticalActivityAsset> entity)
    {
        entity.ToTable("CriticalActivityAsset", "ServiceCatalog");
        
        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new CriticalActivityAssetId(v))
            .ValueGeneratedNever();
       
        entity.HasKey(e => e.Id);
      
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.CriticalActivityId)
            .HasConversion(x => x.Value, x => new CriticalActivityId(x));

        entity.HasOne(x => x.CriticalActivity)
            .WithMany(x => x.CriticalActivityAssets)
            .HasForeignKey(x => x.CriticalActivityId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetId)
            .HasConversion(x => x.Value, x => new AssetId(x));

        entity.HasOne(x => x.Asset)
            .WithMany(x => x.CriticalActivityAssets)
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
