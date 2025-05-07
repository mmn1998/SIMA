using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class WageRateConfiguration : IEntityTypeConfiguration<WageRate>
{
    public void Configure(EntityTypeBuilder<WageRate> entity)
    {
        entity.ToTable("WageRate", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Name).HasMaxLength(200);
        entity.Property(x => x.IsBasedOnPercentage).HasMaxLength(1).IsFixedLength();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.CurrencyOperationTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CurrencyOperationType)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.CurrencyOperationTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftOriginId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftOrigin)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.DraftOriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CurrencyType)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.CurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.DraftTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.DraftType)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.DraftTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.PaymentTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.PaymentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CurrencyPaymentChannelId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CurrencyPaymentChannel)
            .WithMany(x => x.WageRates)
            .HasForeignKey(x => x.CurrencyPaymentChannelId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}