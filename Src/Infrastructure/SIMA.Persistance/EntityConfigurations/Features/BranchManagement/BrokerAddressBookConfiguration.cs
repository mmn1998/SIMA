using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BrokerAddressBookConfiguration : IEntityTypeConfiguration<BrokerAddressBook>
{
    public void Configure(EntityTypeBuilder<BrokerAddressBook> entity)
    {
        entity.ToTable("BrokerAddressBook", "Bank");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.PostalCode).HasMaxLength(20);
        entity.HasIndex(x => x.PostalCode).IsUnique();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AddressTypeId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.AddressType)
            .WithMany(x => x.BrokerAddressBooks)
            .HasForeignKey(x => x.AddressTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BrokerId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.BrokerAddressBooks)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
