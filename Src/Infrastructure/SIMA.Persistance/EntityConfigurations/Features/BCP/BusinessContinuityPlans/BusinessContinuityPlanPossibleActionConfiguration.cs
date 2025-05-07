using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanPossibleActionConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanPossibleAction>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanPossibleAction> entity)
    {
        entity.ToTable("BusinessContinuityPlanPossibleAction", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPossibleActionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.Title).HasMaxLength(200);
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
            .WithMany(x => x.BusinessContinuityPlanPossibleActions)
            .HasForeignKey(x => x.BusinessContinuityPlanId);
    }
}
