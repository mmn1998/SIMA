using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts
{
    public class DraftDestinationConfiguration : IEntityTypeConfiguration<DraftDestination>
    {
        public void Configure(EntityTypeBuilder<DraftDestination> entity)
        {
            entity.ToTable("DraftDestination", "TrustyDraft");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new DraftDestinationId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Code).IsRequired().HasMaxLength(20);
            entity.HasIndex(x => x.Code).IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
