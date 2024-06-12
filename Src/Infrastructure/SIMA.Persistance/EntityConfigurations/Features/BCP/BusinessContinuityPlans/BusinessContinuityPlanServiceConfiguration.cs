using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanServiceConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanService>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanService> entity)
    {
        entity.ToTable("BusinessContinuityPlanService", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityPlanServiceId(v)).ValueGeneratedNever();
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
        entity.HasOne(x => x.BusinessContinuityPlan)
            .WithMany(x => x.BusinessContinuityPlanServices)
            .HasForeignKey(x => x.BusinessContinuityPlanId);
        
        entity.Property(x => x.ServiceId)
            .HasConversion(
            x => x.Value,
            x => new ServiceId(x)
            );
        entity.HasOne(x => x.Service)
            .WithMany(x => x.BusinessContinuityPlanServices)
            .HasForeignKey(x => x.ServiceId);
    }
}
