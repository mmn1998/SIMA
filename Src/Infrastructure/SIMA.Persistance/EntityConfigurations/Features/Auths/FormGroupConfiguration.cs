using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class FormGroupConfiguration : IEntityTypeConfiguration<FormGroup>
{
    public void Configure(EntityTypeBuilder<FormGroup> entity)
    {
        entity.ToTable("FormGroup", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new FormGroupId(v)).ValueGeneratedNever();
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

        entity.Property(x => x.GroupId)
          .HasConversion(
           v => v.Value,
           v => new GroupId(v));

        entity.HasOne(d => d.Group).WithMany(d => d.FormGroups).HasForeignKey(d => d.GroupId);
        entity.HasOne(d => d.Form).WithMany(d => d.FormGroups).HasForeignKey(d => d.FormId);

    }
}
