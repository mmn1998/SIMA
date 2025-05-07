using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class PaymentReceiptInfoConfiguration : IEntityTypeConfiguration<PaymentReceiptInfo>
{
    public void Configure(EntityTypeBuilder<PaymentReceiptInfo> entity)
    {
        entity.ToTable("PaymentReceiptInfo", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new PaymentReceiptInfoId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.PaymentReceiptInfos)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftDocumentId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraftDocument)
            .WithMany(x => x.PaymentReceiptInfos)
            .HasForeignKey(x => x.TrustyDraftDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}