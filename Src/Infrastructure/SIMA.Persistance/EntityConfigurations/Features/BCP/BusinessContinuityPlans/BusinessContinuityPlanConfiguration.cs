using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

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
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.Title).HasMaxLength(200);
        entity.Property(e => e.VersionNumber).HasMaxLength(20);
        entity.Property(e => e.Scope);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
        .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.PlanTypeId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.PlanType)
            .WithMany(x => x.BusinessContinuityPlans)
            .HasForeignKey(x => x.PlanTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
