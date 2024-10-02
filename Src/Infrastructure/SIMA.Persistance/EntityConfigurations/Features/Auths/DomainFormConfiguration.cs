using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class DomainFormConfiguration : IEntityTypeConfiguration<DomainForm>
    {
        public void Configure(EntityTypeBuilder<DomainForm> entity)
        {
            entity.ToTable("DomainForms", "Authentication");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new DomainFormId(v)).ValueGeneratedNever();
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
                .WithMany(d => d.DomainForms)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.DomainId)
             .HasConversion(
              v => v.Value,
              v => new DomainId(v));

            entity.HasOne(d => d.Domain)
                .WithMany(d => d.DomainForms)
                .HasForeignKey(d => d.DomainId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
