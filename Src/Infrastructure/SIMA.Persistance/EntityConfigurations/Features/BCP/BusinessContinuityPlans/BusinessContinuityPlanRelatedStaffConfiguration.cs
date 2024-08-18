using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanRelatedStaffConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanRelatedStaff>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanRelatedStaff> entity)
    {
        entity.ToTable("BusinessContinuityPlanRelatedStaff", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPlanRelatedStaffId(v)).ValueGeneratedNever();
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
            .WithMany(x => x.BusinessContinuityPlanRelatedStaff)
            .HasForeignKey(x => x.BusinessContinuityPlanVersioningId);

        entity.Property(x => x.StaffId)
          .HasConversion(
          x => x.Value,
          x => new StaffId(x)
          );

        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessContinuityPlanRelatedStaff)
            .HasForeignKey(x => x.StaffId);
    }
}