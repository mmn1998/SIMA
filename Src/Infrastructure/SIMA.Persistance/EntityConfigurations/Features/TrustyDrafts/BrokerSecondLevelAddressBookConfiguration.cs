using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class BrokerSecondLevelAddressBookConfiguration : IEntityTypeConfiguration<BrokerSecondLevelAddressBook>
{
    public void Configure(EntityTypeBuilder<BrokerSecondLevelAddressBook> entity)
    {
        entity.ToTable("BrokerSecondLevelAddressBook", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
        entity.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}