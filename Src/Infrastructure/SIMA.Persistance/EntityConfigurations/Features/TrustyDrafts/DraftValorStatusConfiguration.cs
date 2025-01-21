using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class DraftValorStatusConfiguration : IEntityTypeConfiguration<DraftValorStatus>
{
    public void Configure(EntityTypeBuilder<DraftValorStatus> entity)
    {
        entity.ToTable("DraftValorStatus", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DraftValorStatusId(v)).ValueGeneratedNever();
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
