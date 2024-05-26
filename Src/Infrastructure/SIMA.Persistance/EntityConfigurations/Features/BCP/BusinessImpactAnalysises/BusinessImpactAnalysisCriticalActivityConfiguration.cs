using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisCriticalActivityConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisCriticalActivity>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisCriticalActivity> entity)
    {
        entity.ToTable("BCP", "BusinessImpactAnalysisCriticalActivity");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisCriticalActivityId(v)).ValueGeneratedNever();
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
            .WithMany(x => x.BusinessImpactAnalysisCriticalActivities)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
    }
}
