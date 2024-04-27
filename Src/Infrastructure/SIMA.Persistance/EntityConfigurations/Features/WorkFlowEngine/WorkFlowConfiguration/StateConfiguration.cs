using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

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
