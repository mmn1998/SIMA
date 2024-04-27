using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class FormRoleConfiguration : IEntityTypeConfiguration<FormRole>
{
    public void Configure(EntityTypeBuilder<FormRole> entity)
    {
        entity.ToTable("FormRole", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
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

        entity.Property(x => x.RoleId)
          .HasConversion(
           v => v.Value,
           v => new RoleId(v));

        entity.HasOne(d => d.Role).WithMany(d => d.FormRoles).HasForeignKey(d => d.RoleId);
        entity.HasOne(d => d.Form).WithMany(d => d.FormRoles).HasForeignKey(d => d.FormId);

    }
}
