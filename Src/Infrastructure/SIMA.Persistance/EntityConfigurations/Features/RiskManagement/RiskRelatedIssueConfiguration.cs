using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class RiskRelatedIssueConfiguration : IEntityTypeConfiguration<RiskRelatedIssue>
    {
        public void Configure(EntityTypeBuilder<RiskRelatedIssue> entity)
        {
            entity.ToTable("RiskRelatedIssue", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RiskRelatedIssueId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Risk).WithMany(p => p.RiskRelatedIssues)
                .HasForeignKey(d => d.RiskId)
                .HasConstraintName("FK_RiskRelatedIssue_Risk");

            entity.HasOne(d => d.Issue).WithMany(p => p.RiskRelatedIssues)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK_RiskRelatedIssue_Issue");

        }
    }
}
