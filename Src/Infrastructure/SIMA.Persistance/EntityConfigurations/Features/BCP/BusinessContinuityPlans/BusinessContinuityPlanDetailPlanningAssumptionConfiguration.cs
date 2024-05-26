using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanDetailPlanningAssumptionConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanDetailPlanningAssumption>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanDetailPlanningAssumption> entity)
    {
        entity.ToTable("BCP", "BusinessContinuityPlanDetailPlanningAssumption");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityDetailPlanningId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.Title).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessContinuityPlanId)
            .HasConversion(
            x => x.Value,
            x => new BusinessContinuityPlanId(x)
            );
        entity.HasOne(x => x.BusinessContinuityPlan)
            .WithMany(x => x.BusinessContinuityPlanDetailPlanningAssumptions)
            .HasForeignKey(x => x.BusinessContinuityPlanId);
    }
}
