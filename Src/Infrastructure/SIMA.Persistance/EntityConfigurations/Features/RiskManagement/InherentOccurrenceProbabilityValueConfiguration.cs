using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class InherentOccurrenceProbabilityValueConfiguration : IEntityTypeConfiguration<InherentOccurrenceProbabilityValue>
{
    public void Configure(EntityTypeBuilder<InherentOccurrenceProbabilityValue> entity)
    {
        entity.ToTable("InherentOccurrenceProbabilityValue", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Color).IsRequired().HasMaxLength(10);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}public class CurrentOccurrenceProbabilityValueConfiguration : IEntityTypeConfiguration<CurrentOccurrenceProbabilityValue>
{
    public void Configure(EntityTypeBuilder<CurrentOccurrenceProbabilityValue> entity)
    {
        entity.ToTable("CurrentOccurrenceProbabilityValue", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Color).IsRequired().HasMaxLength(10);
        entity.Property(e => e.ValuingIntervalTitle).IsRequired().HasMaxLength(1000);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
