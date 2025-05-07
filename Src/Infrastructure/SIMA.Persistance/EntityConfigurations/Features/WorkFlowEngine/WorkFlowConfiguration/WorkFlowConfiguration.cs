using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

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