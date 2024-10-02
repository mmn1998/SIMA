using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.RequestInquiries
{
    public class RequestInquiryConfiguration : IEntityTypeConfiguration<RequestInquiry>
    {
        public void Configure(EntityTypeBuilder<RequestInquiry> entity)
        {
            entity.ToTable("RequestInquiry", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new RequestInquiryId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.InquieredPrice)
                     .HasColumnType("numeric(18,2)");
            entity.Property(x => x.InvoiceDocumentId)
                .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
            entity.HasOne(x => x.InvoiceDocument)
                .WithMany(x => x.RequestInquiries)
                .HasForeignKey(x => x.InvoiceDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.IsWrittenInquiry).HasMaxLength(1).IsFixedLength();
            entity.Property(e => e.IsContractRequired).HasMaxLength(1).IsFixedLength();
            entity.Property(e => e.IsPrePaymentRequired).HasMaxLength(1).IsFixedLength();

            entity.Property(x => x.CandidatedSupplierId)
               .HasConversion(x => x.Value, x => new CandidatedSupplierId(x));
            entity.HasOne(x => x.CandidatedSupplier)
                .WithMany(x => x.RequestInquiries)
                .HasForeignKey(x => x.CandidatedSupplierId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
