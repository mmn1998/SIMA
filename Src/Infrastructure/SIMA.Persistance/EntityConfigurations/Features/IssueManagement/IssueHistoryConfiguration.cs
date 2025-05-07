using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueHistoryConfiguration : IEntityTypeConfiguration<IssueHistory>
{
    public void Configure(EntityTypeBuilder<IssueHistory> entity)
    {
        entity.ToTable("IssueHistory", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(x => x.IssueId)
               .HasConversion(
                v => v.Value,
                v => new IssueId(v));
        entity.Property(x => x.PerformerUserId)
               .HasConversion(
                v => v.Value,
                v => new UserId(v));
        entity.Property(x => x.SourceStateId)
               .HasConversion(
                v => v.Value,
                v => new StateId(v));
        entity.Property(x => x.TargetStateId)
               .HasConversion(
                v => v.Value,
                v => new StateId(v));
        entity.Property(x => x.SourceStepId)
               .HasConversion(
                v => v.Value,
                v => new StepId(v));
        entity.Property(x => x.TargetStepId)
               .HasConversion(
                v => v.Value,
                v => new StepId(v));
        entity.HasOne(d => d.Issue).WithMany(p => p.IssueHistories)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(d => d.PerformerUser).WithMany(p => p.IssueHistories)
                .HasForeignKey(d => d.PerformerUserId);
        entity.HasOne(d => d.SourceState).WithMany(p => p.SourceIssueHistories)
                .HasForeignKey(d => d.SourceStateId);
        entity.HasOne(d => d.TargetState).WithMany(p => p.TargetIssueHistories)
                .HasForeignKey(d => d.TargetStateId);
        entity.HasOne(d => d.SourceStep).WithMany(p => p.SourceIssueHistories)
                .HasForeignKey(d => d.SourceStepId);
        entity.HasOne(d => d.TargetStep).WithMany(p => p.TargetIssueHistories)
                .HasForeignKey(d => d.TargetStepId);
    }
}
