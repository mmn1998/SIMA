using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class InquiryRequestCurrencyConfiguration : IEntityTypeConfiguration<InquiryRequestCurrency>
{
    public void Configure(EntityTypeBuilder<InquiryRequestCurrency> entity)
    {
        entity.ToTable("InquiryRequestCurrency", "TrustyDraft");


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

        entity.Property(e => e.CurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CurrencyType)
            .WithMany(x => x.InquiryRequestCurrencies)
            .HasForeignKey(x => x.CurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.InquiryRequestId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.InquiryRequest)
            .WithMany(x => x.InquiryRequestCurrencies)
            .HasForeignKey(x => x.InquiryRequestId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}