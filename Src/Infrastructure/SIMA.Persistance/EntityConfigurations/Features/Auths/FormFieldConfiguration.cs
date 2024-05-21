using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class FormFieldConfiguration : IEntityTypeConfiguration<FormField>
{
    public void Configure(EntityTypeBuilder<FormField> entity)
    {
        entity.ToTable("FormField", "Authentication");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new FormFieldId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
      .IsRowVersion()
      .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.Type).HasMaxLength(200);

        entity.Property(x => x.FormId)
          .HasConversion(
           v => v.Value,
           v => new FormId(v));

        entity.HasOne(d => d.Form).WithMany(d => d.FormFields).HasForeignKey(d => d.FormId);



    }
}
