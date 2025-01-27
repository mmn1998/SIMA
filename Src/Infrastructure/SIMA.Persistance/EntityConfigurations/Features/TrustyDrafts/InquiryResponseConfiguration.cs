using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts
{
    public class InquiryResponseConfiguration : IEntityTypeConfiguration<InquiryResponse>
    {
        public void Configure(EntityTypeBuilder<InquiryResponse> entity)
        {
            entity.ToTable("InquiryResponse", "TrustyDraft");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new InquiryResponseId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);


            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(e => e.InquiryRequestId)
                .HasConversion
                    (v => v.Value,
                    v => new(v));
            entity.HasOne(x => x.InquiryRequest)
                .WithMany(x => x.InquiryResponses)
                .HasForeignKey(x => x.InquiryRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.BrokerInquiryStatusId)
                .HasConversion
                    (v => v.Value,
                    v => new(v));
            entity.HasOne(x => x.BrokerInquiryStatus)
                .WithMany(x => x.InquiryResponses)
                .HasForeignKey(x => x.BrokerInquiryStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.InquiryRequestCurrencyId)
                .HasConversion
                    (v => v.Value,
                    v => new(v));
            entity.HasOne(x => x.InquiryRequestCurrency)
                .WithMany(x => x.InquiryResponses)
                .HasForeignKey(x => x.InquiryRequestCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.BrokerId)
                .HasConversion
                    (v => v.Value,
                    v => new(v));
            entity.HasOne(x => x.Broker)
                .WithMany(x => x.InquiryResponses)
                .HasForeignKey(x => x.BrokerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.WageRateId)
        .HasConversion
            (v => v.Value,
            v => new(v));
            entity.HasOne(x => x.WageRate)
                .WithMany(x => x.InquiryResponses)
                .HasForeignKey(x => x.WageRateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
