using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BrokerAccountBookConfiguration : IEntityTypeConfiguration<BrokerAccountBook>
{
    public void Configure(EntityTypeBuilder<BrokerAccountBook> entity)
    {
        entity.ToTable("BrokerAccountBook", "Bank");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.IBANNumber).HasMaxLength(200);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BrokerId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.BrokerAccountBooks)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}