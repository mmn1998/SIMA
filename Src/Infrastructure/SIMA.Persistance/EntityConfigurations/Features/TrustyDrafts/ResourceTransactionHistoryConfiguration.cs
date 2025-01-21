using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class ResourceTransactionHistoryConfiguration : IEntityTypeConfiguration<ResourceTransactionHistory>
{
    public void Configure(EntityTypeBuilder<ResourceTransactionHistory> entity)
    {
        entity.ToTable("ResourceTransactionHistory", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ResourceTransactionHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.IsBlocked).HasMaxLength(1).IsFixedLength();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.FinancialActionTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.FinancialActionType)
            .WithMany(x => x.TransactionHistories)
            .HasForeignKey(x => x.FinancialActionTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.FinancialSupplierId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.FinancialSupplier)
            .WithMany(x => x.TransactionHistories)
            .HasForeignKey(x => x.FinancialSupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.ResourceId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Resource)
            .WithMany(x => x.TransactionHistories)
            .HasForeignKey(x => x.ResourceId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}