using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.Domains.Entities;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> entity)
        {
            entity.ToTable("Form", "Authentication");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new FormId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
          .IsRowVersion()
          .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.Property(x => x.DomainId)
              .HasConversion(
               v => v.Value,
               v => new DomainId(v));
            entity.HasOne(d => d.Domain).WithMany(d => d.Forms).HasForeignKey(d => d.DomainId);



        }
    }
}
