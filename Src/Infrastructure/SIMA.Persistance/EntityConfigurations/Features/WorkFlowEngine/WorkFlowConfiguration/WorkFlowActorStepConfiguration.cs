using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

public class WorkFlowActorStepConfiguration : IEntityTypeConfiguration<WorkFlowActorStep>
{
    public void Configure(EntityTypeBuilder<WorkFlowActorStep> entity)
    {
        entity.ToTable("WorkFlowActorStep", "Project");

        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new WorkFlowActorStepId(v))
            .ValueGeneratedNever();

        entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.StepId)
   .HasConversion(
    v => v.Value,
    v => new StepId(v));
        entity.Property(e => e.StepId).HasColumnName("StepID");
        entity.Property(x => x.WorkFlowActorId)
      .HasConversion(
       v => v.Value,
       v => new WorkFlowActorId(v));
        entity.Property(e => e.WorkFlowActorId).HasColumnName("WorkFlowActorID");

        entity.HasOne(d => d.Step).WithMany(p => p.WorkFlowActorSteps)
            .HasForeignKey(d => d.StepId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_WorkFlowActorStep_Step");

        entity.HasOne(d => d.WorkFlowActor).WithMany(p => p.WorkFlowActorSteps)
            .HasForeignKey(d => d.WorkFlowActorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_WorkFlowActorStep_WorkFlowActor");
    }
}
