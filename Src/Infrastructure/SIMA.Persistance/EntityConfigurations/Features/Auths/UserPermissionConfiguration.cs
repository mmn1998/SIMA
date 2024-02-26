using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> entity)
    {
        entity.ToTable("UserPermission", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserPermissionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.PermissionId).HasConversion(v => v.Value, v => new PermissionId(v));
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));

        entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
            .HasForeignKey(d => d.PermissionId)
            .HasConstraintName("FK_UserPermission_Permission");

        entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserPermission_User");
    }
}
