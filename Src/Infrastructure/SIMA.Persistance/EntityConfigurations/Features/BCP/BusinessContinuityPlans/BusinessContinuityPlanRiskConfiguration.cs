using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanRiskConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanRisk>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanRisk> entity)
    {
        entity.ToTable("BusinessContinuityPlanRisk", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPlanRiskId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessContinuityPlanVersioningId)
            .HasConversion(
            x => x.Value,
            x => new BusinessContinuityPlanVersioningId(x)
            );
        entity.HasOne(x => x.BusinessContinuityPlanVersioning)
            .WithMany(x => x.BusinessContinuityPlanRisks)
            .HasForeignKey(x => x.BusinessContinuityPlanVersioningId);
        
        entity.Property(x => x.RiskId)
            .HasConversion(
            x => x.Value,
            x => new RiskId(x)
            );
        entity.HasOne(x => x.Risk)
            .WithMany(x => x.BusinessContinuityPlanRisks)
            .HasForeignKey(x => x.RiskId);
    }
}