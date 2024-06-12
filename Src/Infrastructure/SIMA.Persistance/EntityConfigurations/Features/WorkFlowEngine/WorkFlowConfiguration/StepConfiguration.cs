using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

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


        entity.Property(x => x.FormId)
            .HasConversion(
            v => v.Value,
            v => new FormId(v));

        entity.Property(x => x.WorkFlowId)
           .HasConversion(
           v => v.Value,
           v => new WorkFlowId(v));

        entity.Property(x => x.ActionTypeId)
          .HasConversion(
          v => v.Value,
          v => new ActionTypeId(v));




        entity.Property(e => e.WorkFlowId).HasColumnName("WorkFlowID");

        entity.HasOne(d => d.Form).WithMany(p => p.Steps)
           .HasForeignKey(d => d.FormId);

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