using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class StatementConfiguration : IEntityTypeConfiguration<Statement>
{
    public void Configure(EntityTypeBuilder<Statement> entity)
    {
        entity.ToTable("Statement", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new StatementId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.TrustyDocumentId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDocument)
            .WithMany(x => x.Statements)
            .HasForeignKey(x => x.TrustyDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.Statements)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}