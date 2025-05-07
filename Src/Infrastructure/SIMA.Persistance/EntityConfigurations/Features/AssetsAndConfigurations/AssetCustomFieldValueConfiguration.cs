using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetCustomFieldValueConfiguration : IEntityTypeConfiguration<AssetCustomFieldValue>
{
    public void Configure(EntityTypeBuilder<AssetCustomFieldValue> entity)
    {
        entity.ToTable("AssetCustomFieldValue", "Asset");

        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        
        entity.HasOne(d => d.AssetCustomField)
            .WithMany(d => d.AssetCustomFieldValue)
            .HasForeignKey(d => d.AssetCustomFieldId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        
        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetCustomFieldValue)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        
    }
}