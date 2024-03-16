using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration
{
    public class WorkFlowConfiguration : IEntityTypeConfiguration<WorkFlow>
    {
        public void Configure(EntityTypeBuilder<WorkFlow> entity)
        {
            entity.ToTable("WorkFlow", "Project");

            //entity.OwnsOne(current => current.Id);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasKey("Id");

            entity.Property(x => x.Id)
.HasColumnName("Id")
.HasConversion(
 v => v.Value,
 v => new WorkFlowId(v))
.ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasComment("")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.ManagerRoleId).HasColumnName("ManagerRoleID");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode();

            entity.Property(x => x.ProjectId)
               .HasConversion(
                v => v.Value,
                v => new ProjectId(v));
            entity.Property(x => x.MainAggregateId)
               .HasConversion(
                v => v.Value,
                v => new MainAggregateId(v));
            entity.Property(x => x.ManagerRoleId)
               .HasConversion(
                v => v.Value,
                v => new RoleId(v));
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithMany(p => p.WorkFlows)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkFlow_Project");

            entity.HasMany(x => x.Steps).WithOne(x => x.WorkFlow).HasForeignKey(x => x.WorkFlowId);
            entity.HasOne(x => x.MainAggregate).WithMany(x => x.WorkFlows).HasForeignKey(x => x.MainAggregateId);
            entity.HasOne(x => x.ManagerRole).WithMany(x => x.WorkFlows).HasForeignKey(x => x.ManagerRoleId);
        }
    }

    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.ToTable("State", "Project");

            entity.HasIndex(e => e.Code, "index_code").IsUnique();

            entity.Property(x => x.Id)
             .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new StateId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.Code).HasMaxLength(50).IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();


            entity.Property(x => x.WorkFlowId)
           .HasConversion(
            v => v.Value,
            v => new WorkFlowId(v));
            entity.Property(e => e.WorkFlowId).HasColumnName("WorkFlowID");

            entity.HasOne(d => d.WorkFlow).WithMany(p => p.States)
                .HasForeignKey(d => d.WorkFlowId)
                .HasConstraintName("FK_State_WorkFlow");
        }
    }

    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> entity)
        {
            entity.ToTable("Step", "Project");


            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new StepId(v))
            .ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(x => x.StateId)
                .HasConversion(
                v => v.Value,
                v => new StateId(v));
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.Property(x => x.WorkFlowId)
               .HasConversion(
               v => v.Value,
               v => new WorkFlowId(v));

            entity.Property(x => x.ActionTypeId)
              .HasConversion(
              v => v.Value,
              v => new ActionTypeId(v));


         

            entity.Property(e => e.WorkFlowId).HasColumnName("WorkFlowID");

            entity.HasOne(d => d.State).WithMany(p => p.Steps)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Step_State");

            entity.HasOne(d => d.ActionType).WithMany(p => p.steps)
               .HasForeignKey(d => d.ActionTypeId);

            entity.HasOne(d => d.WorkFlow).WithMany(p => p.Steps)
                .HasForeignKey(d => d.WorkFlowId)
                .HasConstraintName("FK_Step_WorkFlow");
            entity.HasMany(s => s.SourceProgresses)
 .WithOne(p => p.Source)
 .HasForeignKey(p => p.SourceId)
 .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.TargetProgresses)
                .WithOne(p => p.Target)
                .HasForeignKey(p => p.TargetId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

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
}
