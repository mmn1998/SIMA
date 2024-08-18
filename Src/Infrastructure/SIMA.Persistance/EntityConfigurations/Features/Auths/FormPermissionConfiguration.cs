using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class FormPermissionConfiguration : IEntityTypeConfiguration<FormPermission>
{
    public void Configure(EntityTypeBuilder<FormPermission> entity)
    {
        entity.ToTable("FormPermission", "Authentication");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new FormPermissionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.FormId)
         .HasConversion(
          v => v.Value,
          v => new FormId(v));

        entity.HasOne(d => d.Form)
            .WithMany(d => d.FormPermissions)
            .HasForeignKey(d => d.FormId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PermissionId)
         .HasConversion(
          v => v.Value,
          v => new PermissionId(v));

        entity.HasOne(d => d.Permission)
            .WithMany(d => d.FormPermissions)
            .HasForeignKey(d => d.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
