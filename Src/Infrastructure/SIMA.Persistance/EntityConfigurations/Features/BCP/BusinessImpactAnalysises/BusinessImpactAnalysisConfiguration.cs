using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysis>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysis> entity)
    {
        entity.ToTable("BusinessImpactAnalysis", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(x => x.ServiceId)
            .HasConversion(x => x.Value, x => new ServiceId(x));
        entity.HasOne(x => x.Service)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.ServiceId);

        entity.Property(x => x.RecoveryPointObjectiveId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.RecoveryPointObjective)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.RecoveryPointObjectiveId);

        entity.Property(x => x.TimeMeasurementId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.TimeMeasurement)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.TimeMeasurementId);
           
    }
}