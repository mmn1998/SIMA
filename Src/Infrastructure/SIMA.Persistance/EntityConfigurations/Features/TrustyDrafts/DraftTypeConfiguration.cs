using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class DraftTypeConfiguration : IEntityTypeConfiguration<DraftType>
{
    public void Configure(EntityTypeBuilder<DraftType> entity)
    {
        entity.ToTable("DraftType", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DraftTypeId(v)).ValueGeneratedNever();
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
