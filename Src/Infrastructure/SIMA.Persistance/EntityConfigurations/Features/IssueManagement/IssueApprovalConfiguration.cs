using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

public class IssueApprovalConfiguration : IEntityTypeConfiguration<IssueApproval>
{
    public void Configure(EntityTypeBuilder<IssueApproval> entity)
    {
        entity.ToTable("IssueApproval", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueApprovalId(v))
    .ValueGeneratedNever();

        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");

        entity.Property(e => e.Description);
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.HasOne(d => d.Issue).WithMany(p => p.IssueApprovals)
                .HasForeignKey(d => d.IssueId )
                .HasConstraintName("FK_IssueApproval_Issue");

        entity.HasOne(d => d.WorkflowActor).WithMany(p => p.IssueApprovals)
                .HasForeignKey(d => d.WorkflowActorId)
                .HasConstraintName("FK_IssueApproval_WorkflowActor").OnDelete(DeleteBehavior.NoAction);

        entity.HasOne(d => d.StepApprovalOption).WithMany(p => p.IssueApprovals)
               .HasForeignKey(d => d.StepApprovalOptionId)
               .HasConstraintName("FK_IssueApproval_StepApprovalOption").OnDelete(DeleteBehavior.NoAction);




    }
}
