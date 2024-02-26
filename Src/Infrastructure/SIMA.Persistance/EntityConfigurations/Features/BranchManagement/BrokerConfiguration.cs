using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BrokerConfiguration : IEntityTypeConfiguration<Broker>
{
    public void Configure(EntityTypeBuilder<Broker> entity)
    {
        entity.ToTable("Broker", "Bank");
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BrokerId(v)).ValueGeneratedNever();
        entity.Property(e => e.Address).HasMaxLength(500);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ExpireDate).HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.PhoneNumber).HasMaxLength(20);

        ///todo
        entity.HasOne(d => d.BrokerType).WithMany(p => p.Brokers)
            .HasForeignKey(d => d.BrokerTypeId)
            .HasConstraintName("FK_Broker_BrokerType");
    }
}
