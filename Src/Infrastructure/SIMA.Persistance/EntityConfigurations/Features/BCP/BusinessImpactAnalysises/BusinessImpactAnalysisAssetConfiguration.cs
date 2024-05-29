using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisAssetConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisAsset>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisAsset> entity)
    {
        entity.ToTable("BusinessImpactAnalysisAsset", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisAssetId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(
            x => x.Value,
            x => new BusinessImpactAnalysisId(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisAssets)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
    }
}
