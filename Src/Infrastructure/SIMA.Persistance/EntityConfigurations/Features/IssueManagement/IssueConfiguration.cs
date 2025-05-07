using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> entity)
    {
        entity.ToTable("Issue", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Description).IsUnicode();
        entity.Property(e => e.IsFinished).HasMaxLength(1).IsFixedLength();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IssueTypeId)
       .HasConversion(
        v => v.Value,
        v => new IssueTypeId(v));
        entity.HasOne(d => d.IssueType).WithMany(p => p.Issues)
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
        entity.HasOne(d => d.IssuePriority).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssuePriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssueWeightCategoryId)
       .HasConversion(
        v => v.Value,
        v => new IssueWeightCategoryId(v));
        entity.Property(x => x.AssigneeId)
       .HasConversion(
        v => v.Value,
        v => new UserId(v));
        entity.Property(x => x.RequesterId)
            .HasConversion(x => x.Value, x => new UserId(x));
        entity.HasOne(x => x.Requester)
            .WithMany(x => x.Issues)
            .HasForeignKey(x => x.RequesterId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.IssueWeightCategory).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueWeightCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(x => x.CurrenStep)
            .WithMany(p => p.Issues)
            .HasForeignKey(x => x.CurrenStepId);
        entity.HasOne(x => x.CurrentState)
            .WithMany(p => p.Issues)
            .HasForeignKey(x => x.CurrentStateId);
        entity.HasOne(x => x.MainAggregate)
            .WithMany(p => p.Issues)
            .HasForeignKey(x => x.MainAggregateId);
        
    }
}
