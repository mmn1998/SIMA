using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class InherentOccurrenceProbabilityConfiguration : IEntityTypeConfiguration<InherentOccurrenceProbability>
{
    public void Configure(EntityTypeBuilder<InherentOccurrenceProbability> entity)
    {
        entity.ToTable("InherentOccurrenceProbability", "RiskManagement");

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

        entity.Property(x => x.MatrixAValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.MatrixAValue)
            .WithMany(x => x.InherentOccurrenceProbabilities)
            .HasForeignKey(x => x.MatrixAValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.InherentOccurrenceProbabilityValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.InherentOccurrenceProbabilityValue)
            .WithMany(x => x.InherentOccurrenceProbabilities)
            .HasForeignKey(x => x.InherentOccurrenceProbabilityValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ScenarioHistoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ScenarioHistory)
            .WithMany(x => x.InherentOccurrenceProbabilities)
            .HasForeignKey(x => x.ScenarioHistoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}