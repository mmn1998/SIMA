using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans
{
    public class BusinessContinuityPlanResponsibleConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanResponsible>
    {
        public void Configure(EntityTypeBuilder<BusinessContinuityPlanResponsible> entity)
        {
            entity.ToTable("BusinessContinuityPlanResponsible", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new BusinessContinuityPlanResponsibleId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.IsForBackup).HasMaxLength(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.BusinessContinuityPlanId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.BusinessContinuityPlan)
                .WithMany(x => x.BusinessContinuityPlanResponsibles)
                .HasForeignKey(x => x.BusinessContinuityPlanId);

            entity.Property(x => x.StaffId)
                .HasConversion(
                x => x.Value,
                x => new StaffId(x)
                );
            entity.HasOne(x => x.Staff)
                .WithMany(x => x.BusinessContinuityPlanResponsibles)
                .HasForeignKey(x => x.StaffId);

            entity.Property(x => x.PlanResponsibilityId)
                .HasConversion(
                x => x.Value,
                x => new PlanResponsibilityId(x)
                );
            entity.HasOne(x => x.PlanResponsibility)
                .WithMany(x => x.BusinessContinuityPlanResponsibles)
                .HasForeignKey(x => x.PlanResponsibilityId);

            entity.Property(x => x.DepartmentId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.Department)
                .WithMany(x => x.BusinessContinuityPlanResponsibles)
                .HasForeignKey(x => x.DepartmentId);

            entity.Property(x => x.BranchId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.Branch)
                .WithMany(x => x.BusinessContinuityPlanResponsibles)
                .HasForeignKey(x => x.BranchId);
        }
    }
}
