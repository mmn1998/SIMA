using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanConfiguration : IEntityTypeConfiguration<BusinessContinuityPlan>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlan> entity)
    {
        entity.ToTable("BusinessContinuityPlan", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPlanId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
        .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Title).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Scope).IsUnicode();

        entity.Property(e => e.BusinessContinuityStrategyId)
            .HasConversion(
            x => x.Value,
            x => new BusinessContinuityStrategyId(x)
            );
        entity.Property(e => e.ExecutiveResponsibleId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.Property(e => e.PlanOwnerId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.Property(e => e.RecoveryDeputyId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.Property(e => e.RecoveryManagerId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );

        entity.HasOne(x => x.BusinessContinuityStrategy)
            .WithMany(x => x.BusinessContinuityPlans)
            .HasForeignKey(x => x.BusinessContinuityStrategyId);

        entity.HasOne(x => x.PlanOwner)
            .WithMany(x => x.BusinessContinuityPlanOwners)
            .HasForeignKey(x => x.PlanOwnerId);
        
        entity.HasOne(x => x.ExecutiveResponsible)
            .WithMany(x => x.BusinessContinuityPlanExecutiveResponsibles)
            .HasForeignKey(x => x.ExecutiveResponsibleId);

        entity.HasOne(x => x.RecoveryManager)
            .WithMany(x => x.BusinessContinuityPlanRecoveryManagers)
            .HasForeignKey(x => x.RecoveryManagerId);
        
        entity.HasOne(x => x.RecoveryDeputy)
            .WithMany(x => x.BusinessContinuityPlanRecoveryDeputy)
            .HasForeignKey(x => x.RecoveryDeputyId);
    }
}
