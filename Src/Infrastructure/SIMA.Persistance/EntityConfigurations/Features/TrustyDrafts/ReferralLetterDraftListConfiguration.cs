using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class ReferralLetterDraftListConfiguration : IEntityTypeConfiguration<ReferralLetterDraftList>
{
    public void Configure(EntityTypeBuilder<ReferralLetterDraftList> entity)
    {
        entity.ToTable("ReferralLetterDraftList", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ReferralLetterDraftListId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.ReferralLetterId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.ReferralLetter)
            .WithMany(x => x.ReferralLetterDraftList)
            .HasForeignKey(x => x.ReferralLetterId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.ReferralLetterDraftList)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}