using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class GroupPermissionConfiguration : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> entity)
        {
            entity.ToTable("GroupPermission", "Authentication");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new GroupPermissionId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.PermissionId).HasConversion(v => v.Value, v => new PermissionId(v));
            entity.Property(e => e.GroupId).HasConversion(v => v.Value, v => new GroupId(v));

            entity.HasOne(d => d.Group).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_GroupPermission_Groups");

            entity.HasOne(d => d.Permission).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_GroupPermission_Permission");
        }
    }
}
