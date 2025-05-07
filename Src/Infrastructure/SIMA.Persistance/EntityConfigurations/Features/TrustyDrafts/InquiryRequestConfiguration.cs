using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class InquiryRequestConfiguration : IEntityTypeConfiguration<InquiryRequest>
{
    public void Configure(EntityTypeBuilder<InquiryRequest> entity)
    {
        entity.ToTable("InquiryRequest", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(x => x.BeneficiaryName).HasMaxLength(200);
        entity.Property(x => x.ReferenceNumber).HasMaxLength(24);
        entity.Property(x => x.DraftOrderNumber).HasMaxLength(8);
        entity.Property(x => x.ProformaNumber).HasMaxLength(20);

        entity.HasIndex(x => x.ReferenceNumber).IsUnique();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.PaymentTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.InquiryRequests)
            .HasForeignKey(x => x.PaymentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CustomerId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.InquiryRequests)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BranchId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Branch)
            .WithMany(x => x.InquiryRequests)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftOriginId)
            .HasConversion
                (v => v.Value,
                v => new(v))
                ;
        entity.HasOne(x => x.DraftOrigin)
            .WithMany(x => x.InquiryRequests)
            .HasForeignKey(x => x.DraftOriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.ProformaCurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v))
                ;
        entity.HasOne(x => x.ProformaCurrencyType)
            .WithMany(x => x.InquiryRequests)
            .HasForeignKey(x => x.ProformaCurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
