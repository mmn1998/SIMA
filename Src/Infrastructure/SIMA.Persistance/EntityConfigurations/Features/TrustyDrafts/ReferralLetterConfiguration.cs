using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class ReferralLetterConfiguration : IEntityTypeConfiguration<ReferralLetter>
{
    public void Configure(EntityTypeBuilder<ReferralLetter> entity)
    {
        entity.ToTable("ReferralLetter", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ReferralLetterId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.LetterNumber).HasMaxLength(20);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(e => e.BrokerId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.ReferralLetters)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
          .HasConversion
              (v => v.Value,
              v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.ReferralLetters)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.LetterDocumentId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.LetterDocument)
            .WithMany(x => x.LetterDocuments)
            .HasForeignKey(x => x.LetterDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
