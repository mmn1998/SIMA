using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class BrokerAddressInfoConfiguration : IEntityTypeConfiguration<BrokerAddressInfo>
{
    public void Configure(EntityTypeBuilder<BrokerAddressInfo> entity)
    {
        entity.ToTable("BrokerAddressInfo", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BrokerAddressInfoId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.IsLastConfirmedAddress).HasMaxLength(1).IsFixedLength();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.BrokerAccountBookId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.BrokerAccountBook)
            .WithMany(x => x.BrokerAddressInfos)
            .HasForeignKey(x => x.BrokerAccountBookId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerAddressBookId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.BrokerAddressBook)
            .WithMany(x => x.BrokerAddressInfos)
            .HasForeignKey(x => x.BrokerAddressBookId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerPhoneBookId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.BrokerPhoneBook)
            .WithMany(x => x.BrokerAddressInfos)
            .HasForeignKey(x => x.BrokerPhoneBookId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.BrokerAddressInfos)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}