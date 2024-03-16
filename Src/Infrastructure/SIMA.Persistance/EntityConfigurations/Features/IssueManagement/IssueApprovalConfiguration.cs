using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pipelines.Sockets.Unofficial.Arenas;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Core.Entities;

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
        entity.Property(x => x.WorkflowStepId)
                .HasConversion(
            v => v.Value,
            v => new StepId(v));
        entity.Property(x => x.WorkflowActorId)
                .HasConversion(
            v => v.Value,
            v => new WorkFlowActorId(v));
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.Description).IsUnicode();
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasOne(x => x.WorkflowStep)
            .WithMany(x => x.IssueApprovals)
                .HasForeignKey(x => x.WorkflowStepId);
        entity.HasOne(x => x.WorkflowActor)
            .WithMany(x => x.IssueApprovals)
                .HasForeignKey(x => x.WorkflowActorId);

    }
}
