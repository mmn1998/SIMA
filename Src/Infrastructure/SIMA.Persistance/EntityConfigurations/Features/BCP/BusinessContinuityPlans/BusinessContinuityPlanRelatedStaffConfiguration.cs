using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanRelatedStaffConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanRelatedStaff>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanRelatedStaff> entity)
    {
        entity.ToTable("BusinessContinuityPlanRelatedStaff", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
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
            x => new(x)
            );
        entity.HasOne(x => x.BusinessContinuityPlan)
            .WithMany(x => x.BusinessContinuityPlanRelatedStaff)
            .HasForeignKey(x => x.BusinessContinuityPlanId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.StaffId)
          .HasConversion(
          x => x.Value,
          x => new(x)
          );

        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessContinuityPlanRelatedStaff)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}