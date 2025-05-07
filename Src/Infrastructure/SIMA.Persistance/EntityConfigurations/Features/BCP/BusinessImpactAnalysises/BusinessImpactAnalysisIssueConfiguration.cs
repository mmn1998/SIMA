using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisIssueConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisIssue>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisIssue> entity)
    {
        entity.ToTable("BusinessImpactAnalysisIssue", "BCP");
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

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisIssues)
            .HasForeignKey(x => x.BusinessImpactAnalysisId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IssueId)
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.BusinessImpactAnalysisIssues)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
