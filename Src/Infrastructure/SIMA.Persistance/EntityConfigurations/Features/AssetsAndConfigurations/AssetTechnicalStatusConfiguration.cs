using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetTechnicalStatusConfiguration : IEntityTypeConfiguration<AssetTechnicalStatus>
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
