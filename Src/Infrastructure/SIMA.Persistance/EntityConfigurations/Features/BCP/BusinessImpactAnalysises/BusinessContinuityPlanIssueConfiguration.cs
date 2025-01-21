using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessContinuityPlanIssueConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanIssue>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanIssue> entity)
    {
        entity.ToTable("BusinessContinuityPlanIssue", "BCP");
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
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.BusinessContinuityPlan)
            .WithMany(x => x.BusinessContinuityPlanIssues)
            .HasForeignKey(x => x.BusinessContinuityPlanId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IssueId)
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.BusinessContinuityPlanIssues)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}