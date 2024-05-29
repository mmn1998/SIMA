using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanStaffConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanStaff>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanStaff> entity)
    {
        entity.ToTable("BusinessContinuityPlanStaff", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPlanStaffId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
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
        entity.Property(x => x.StaffId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );

        entity.HasOne(x => x.BusinessContinuityPlan)
            .WithMany(x => x.BusinessContinuityPlanStaff)
            .HasForeignKey(x => x.BusinessContinuityPlanId);

        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessContinuityPlanStaff)
            .HasForeignKey(x => x.StaffId);
    }
}