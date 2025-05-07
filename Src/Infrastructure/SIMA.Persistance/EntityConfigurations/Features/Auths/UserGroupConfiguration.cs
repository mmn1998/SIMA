using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> entity)
    {
        entity.ToTable("UserGroup", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserGroupId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.GroupId).HasConversion(v => v.Value, v => new GroupId(v));
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.HasOne(d => d.Group).WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.GroupId)
            .HasConstraintName("FK_UserGroup_Groups");

        entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserGroup_User");
    }
}
