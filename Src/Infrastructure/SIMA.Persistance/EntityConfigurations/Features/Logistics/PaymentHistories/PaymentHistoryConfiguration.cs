using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.PaymentHistories;

public class PaymentHistoryConfiguration : IEntityTypeConfiguration<PaymentHistory>
{
    public void Configure(EntityTypeBuilder<PaymentHistory> entity)
    {
        entity.ToTable("PaymentHistory", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new PaymentHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.IsPrePayment).HasMaxLength(1);

        entity.Property(x => x.PaymentTypeId)
            .HasConversion(x => x.Value, x => new PaymentTypeId(x));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentTypeId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.OrderingId)
            .HasConversion(x => x.Value, x => new OrderingId(x));
        entity.HasOne(x => x.Ordering)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.OrderingId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PaymentCommandId)
            .HasConversion(x => x.Value, x => new PaymentCommandId(x));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentTypeId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PaymentDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.PaymentDocument)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
