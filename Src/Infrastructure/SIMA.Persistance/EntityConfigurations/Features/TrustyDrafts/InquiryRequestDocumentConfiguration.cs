using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class InquiryRequestDocumentConfiguration : IEntityTypeConfiguration<InquiryRequestDocument>
{
    public void Configure(EntityTypeBuilder<InquiryRequestDocument> entity)
    {
        entity.ToTable("InquiryRequestDocument", "TrustyDraft");


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
            .WithMany(x => x.InquiryRequestDocuments)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.InquiryRequestId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.InquiryRequest)
            .WithMany(x => x.InquiryRequestDocuments)
            .HasForeignKey(x => x.InquiryRequestId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}