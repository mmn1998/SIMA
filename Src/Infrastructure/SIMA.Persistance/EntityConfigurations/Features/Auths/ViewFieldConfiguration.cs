using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ViewLists.Entities;
using SIMA.Domain.Models.Features.Auths.ViewLists.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class ViewFieldConfiguration : IEntityTypeConfiguration<ViewField>
    {
        public void Configure(EntityTypeBuilder<ViewField> entity)
        {
            entity.ToTable("ViewField", "Authentication");

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ViewFieldId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(x => x.ViewId)
         .HasConversion(
          v => v.Value,
          v => new ViewId(v));

            entity.HasOne(d => d.ViewList).WithMany(d => d.ViewFields).HasForeignKey(d => d.ViewId);
        }
    }
}
