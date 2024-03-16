using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;


namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ProjectConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {

            entity.ToTable("Project", "Project");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
            v => v.Value,
            v => new ProjectId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(x => x.DomainId)
            .HasConversion(
                v => v.Value,
                v => new DomainId(v));
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.Code)
                        .HasMaxLength(20).IsUnicode();
            entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
            entity.Property(e => e.DomainId).HasColumnName("DomainID");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.Name)
                        .HasMaxLength(50);
            entity.HasOne(x => x.Domain)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.DomainId);
        }
    }
    public class ProjectGroupConfiguration : IEntityTypeConfiguration<ProjectGroup>
    {
        public void Configure(EntityTypeBuilder<ProjectGroup> entity)
        {
            entity.ToTable("ProjectGroup", "Project");
            entity.Property(x => x.Id)
                .HasColumnName("Id")
                .HasConversion(
                 v => v.Value,
                 v => new ProjectGroupId(v))
                .ValueGeneratedNever();
            entity.Property(x => x.GroupId)
                .HasConversion(
                 v => v.Value,
                 v => new GroupId(v));
            entity.Property(e => e.ActiveStatusId);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupId);
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(x => x.ProjectId)
            .HasConversion(
            v => v.Value,
            v => new ProjectId(v));
            entity.Property(e => e.ProjectId);
            entity.HasOne(x => x.Project)
                .WithMany(x => x.ProjectGroups)
                .HasForeignKey(x => x.ProjectId);
            entity.HasOne(x => x.Group)
                .WithMany(x => x.ProjectGroups)
                .HasForeignKey(x => x.GroupId);
        }
    }
    public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> entity)
        {
            entity.ToTable("ProjectMember", "Project");

            entity.Property(x => x.Id)
                .HasColumnName("Id")
                .HasConversion(
                 v => v.Value,
                 v => new ProjectMemberId(v))
                .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAdminProject)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsManager)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(x => x.ProjectId)
                    .HasConversion(
                    v => v.Value,
                    v => new ProjectId(v));
            entity.Property(x => x.UserId)
                    .HasConversion(
                    v => v.Value,
                    v => new UserId(v));
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectMember_Project");
            entity.HasOne(d => d.User).WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId);                
        }
    }
}
