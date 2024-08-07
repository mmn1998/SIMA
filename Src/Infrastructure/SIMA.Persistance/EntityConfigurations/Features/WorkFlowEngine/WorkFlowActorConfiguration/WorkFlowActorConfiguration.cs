using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowActorConfiguration
{
    public class WorkFlowActorConfiguration : IEntityTypeConfiguration<WorkFlowActor>
    {
        public void Configure(EntityTypeBuilder<WorkFlowActor> entity)
        {

            entity.ToTable("WorkFlowActor", "Project");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
         .HasConversion(
             v => v.Value,
             v => new WorkFlowActorId(v))
         .ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId);
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode();
            entity.HasOne(x => x.WorkFlow)
               .WithMany(x => x.WorkFlowActors)
               .HasForeignKey(x => x.WorkFlowId);
            entity.Property(e => e.IsDirectManagerOfIssueCreator)
               .HasColumnType("char(1)");
            entity.Property(e => e.IsEveryOne).HasColumnType("char(1)");

        }
    }

    public class WorkFlowActorGroupConfiguration : IEntityTypeConfiguration<WorkFlowActorGroup>
    {
        public void Configure(EntityTypeBuilder<WorkFlowActorGroup> entity)
        {

            entity.ToTable("WorkFlowActorGroup", "Project");

            entity.Property(x => x.Id)
                .HasConversion(
                    v => v.Value,
                    v => new WorkFlowActorGroupId(v))
                .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupId)
                .HasConversion(
                x=>x.Value,
                x=> new GroupId(x));

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.WorkFlowActorId)
          .HasConversion(
           v => v.Value,
           v => new WorkFlowActorId(v));

            entity.HasOne(x => x.Group)
                .WithMany(x => x.WorkFlowActorGroups)
                .HasForeignKey(x => x.GroupId);
            entity.HasOne(x => x.WorkFlowActor)
                .WithMany(x => x.WorkFlowActorGroups)
                .HasForeignKey(x => x.WorkFlowActorId);

        }
    }

    public class WorkFlowActorRoleConfiguration : IEntityTypeConfiguration<WorkFlowActorRole>
    {
        public void Configure(EntityTypeBuilder<WorkFlowActorRole> entity)
        {

            entity.ToTable("WorkFlowActorRole", "Project");

            entity.Property(x => x.Id).HasColumnName("Id")
            .HasConversion(v => v.Value, v => new WorkFlowActorRoleId(v))
            .ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.RoleId)
                .HasConversion(
                x=>x.Value,
                x=> new RoleId(x)
                );
            entity.Property(x => x.WorkFlowActorId)
           .HasConversion(
            v => v.Value,
            v => new WorkFlowActorId(v));
            entity.Property(e => e.WorkFlowActorId);
            entity.HasOne(e => e.Role)
                .WithMany(x => x.WorkFlowActorRoles)
                .HasForeignKey(e => e.RoleId);
            entity.HasOne(d => d.WorkFlowActor).WithMany(p => p.WorkFlowActorRoles)
                .HasForeignKey(d => d.WorkFlowActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkFlowActorRole_WorkFlowActor");

        }
    }

    public class WorkFlowActorUserConfiguration : IEntityTypeConfiguration<WorkFlowActorUser>
    {
        public void Configure(EntityTypeBuilder<WorkFlowActorUser> entity)
        {

            entity.ToTable("WorkFlowActorUser", "Project");

            entity.Property(x => x.Id).HasColumnName("Id")
            .HasConversion(v => v.Value, v => new WorkFlowActorUserId(v))
            .ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.WorkFlowActorId)
            .HasConversion(
             v => v.Value,
             v => new WorkFlowActorId(v));
            entity.Property(x => x.UserId)
            .HasConversion(
             v => v.Value,
             v => new UserId(v));

            entity.HasOne(d => d.User).WithMany(p => p.WorkFlowActorUsers)
                .HasForeignKey(d => d.UserId);
                entity.HasOne(d => d.WorkFlowActor).WithMany(p => p.WorkFlowActorUsers)
                .HasForeignKey(d => d.WorkFlowActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkFlowActorUser_WorkFlowActor");

        }
    }


}
