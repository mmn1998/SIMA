using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class CurrentOccurrenceProbabilityConfiguration : IEntityTypeConfiguration<CurrentOccurrenceProbability>
{
    public void Configure(EntityTypeBuilder<CurrentOccurrenceProbability> entity)
    {
        entity.ToTable("CurrentOccurrenceProbability", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.FrequencyId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.Frequency)
            .WithMany(x => x.CurrentOccurrenceProbabilities)
            .HasForeignKey(x => x.FrequencyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CurrentOccurrenceProbabilityValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.CurrentOccurrenceProbabilityValue)
            .WithMany(x => x.CurrentOccurrenceProbabilities)
            .HasForeignKey(x => x.CurrentOccurrenceProbabilityValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.InherentOccurrenceProbabilityValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.InherentOccurrenceProbabilityValue)
            .WithMany(x => x.CurrentOccurrenceProbabilities)
            .HasForeignKey(x => x.InherentOccurrenceProbabilityValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}