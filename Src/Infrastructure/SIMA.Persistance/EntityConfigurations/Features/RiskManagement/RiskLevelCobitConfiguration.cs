using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskLevelCobitConfiguration : IEntityTypeConfiguration<RiskLevelCobit>
{
    public void Configure(EntityTypeBuilder<RiskLevelCobit> entity)
    {
        entity.ToTable("RiskLevelCobit", "RiskManagement");

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

        entity.Property(x => x.SeverityId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.Severity)
            .WithMany(x => x.RiskLevelCobits)
            .HasForeignKey(x => x.SeverityId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RiskLevelId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.RiskLevel)
            .WithMany(x => x.RiskLevelCobits)
            .HasForeignKey(x => x.RiskLevelId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CurrentOccurrenceProbabilityValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.CurrentOccurrenceProbabilityValue)
            .WithMany(x => x.RiskLevelCobits)
            .HasForeignKey(x => x.CurrentOccurrenceProbabilityValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}