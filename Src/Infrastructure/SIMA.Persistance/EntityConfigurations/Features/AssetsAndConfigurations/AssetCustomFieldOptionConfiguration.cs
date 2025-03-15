using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetCustomFieldOptionConfiguration :IEntityTypeConfiguration<AssetCustomFieldOption>
{
    public void Configure(EntityTypeBuilder<AssetCustomFieldOption> entity)
    {
        entity.ToTable("AssetCustomFieldOption", "Asset");

        
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new AssetCustomFieldOptionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.HasOne(f=>f.AssetCustomField).WithMany(f=>f.AssetCustomFieldOption).HasForeignKey(f=>f.AssetCustomFieldId).OnDelete(DeleteBehavior.Restrict);
    }
}