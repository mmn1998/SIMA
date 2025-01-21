using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BrokerPhoneBookConfiguration : IEntityTypeConfiguration<BrokerPhoneBook>
{
    public void Configure(EntityTypeBuilder<BrokerPhoneBook> entity)
    {
        entity.ToTable("BrokerPhoneBook", "Bank");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.PhoneNumber).HasMaxLength(20);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.PhoneTypeId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.PhoneType)
            .WithMany(x => x.BrokerPhoneBooks)
            .HasForeignKey(x => x.PhoneTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BrokerId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.BrokerPhoneBooks)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}