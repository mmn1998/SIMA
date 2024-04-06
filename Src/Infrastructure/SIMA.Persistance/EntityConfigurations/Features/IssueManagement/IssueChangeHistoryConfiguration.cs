using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueChangeHistoryConfiguration : IEntityTypeConfiguration<IssueChangeHistory>
{

    public void Configure(EntityTypeBuilder<IssueChangeHistory> entity)
    {
        entity.ToTable("IssueChangeHistory", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueChangeHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");

        entity.Property(e => e.Description).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IssueTypeId)
       .HasConversion(
        v => v.Value,
        v => new IssueTypeId(v));
        entity.HasOne(d => d.IssueType).WithMany(p => p.IssueChangeHistories)
                .HasForeignKey(d => d.IssueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssuePriorityId)
       .HasConversion(
        v => v.Value,
        v => new IssuePriorityId(v));
        entity.Property(x => x.CurrenStepId)
       .HasConversion(
        v => v.Value,
        v => new StepId(v));
        entity.Property(x => x.CurrentStateId)
       .HasConversion(
        v => v.Value,
        v => new StateId(v));
        entity.Property(x => x.CurrentWorkflowId)
       .HasConversion(
        v => v.Value,
        v => new WorkFlowId(v));
        entity.Property(x => x.MainAggregateId)
       .HasConversion(
        v => v.Value,
        v => new MainAggregateId(v));
        entity.HasOne(d => d.IssuePriority).WithMany(p => p.IssueChangeHistories)
                .HasForeignKey(d => d.IssuePriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssueWeightCategoryId)
       .HasConversion(
        v => v.Value,
        v => new IssueWeightCategoryId(v));
        entity.HasOne(d => d.IssueWeightCategory).WithMany(p => p.IssueChangeHistories)
                .HasForeignKey(d => d.IssueWeightCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(x => x.CurrenStep).WithMany(p => p.IssueChangeHistories)
            .HasForeignKey(x => x.CurrenStepId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(x => x.CurrentState).WithMany(p => p.IssueChangeHistories)
            .HasForeignKey(x => x.CurrentStateId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(x => x.CurrentWorkflow).WithMany(p => p.IssueChangeHistories)
            .HasForeignKey(x => x.CurrentWorkflowId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(x => x.MainAggregate).WithMany(p => p.IssueChangeHistories)
            .HasForeignKey(x => x.MainAggregateId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(x => x.Issue).WithMany(p => p.IssueChangeHistories)
            .HasForeignKey(x => x.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
