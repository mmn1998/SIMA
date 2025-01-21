using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessContinuityStrategyIssueConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyIssue>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyIssue> entity)
    {
        entity.ToTable("BusinessContinuityStrategyIssue", "BCP");
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

        entity.Property(x => x.BusinessContinuityStrategyId)
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.BusinessContinuityStrategy)
            .WithMany(x => x.BusinessContinuityStrategyIssues)
            .HasForeignKey(x => x.BusinessContinuityStrategyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IssueId)
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.BusinessContinuityStrategyIssues)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}