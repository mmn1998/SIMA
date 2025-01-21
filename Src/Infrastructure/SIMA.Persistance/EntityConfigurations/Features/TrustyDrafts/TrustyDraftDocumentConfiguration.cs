using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class TrustyDraftDocumentConfiguration : IEntityTypeConfiguration<TrustyDraftDocument>
{
    public void Configure(EntityTypeBuilder<TrustyDraftDocument> entity)
    {
        entity.ToTable("TrustyDraftDocument", "TrustyDraft");


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

        entity.Property(e => e.DocumentId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.TrustyDraftDocuments)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.TrustyDraftDocuments)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}