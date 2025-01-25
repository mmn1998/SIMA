using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class SeverityConfiguration : IEntityTypeConfiguration<Severity>
{
    public void Configure(EntityTypeBuilder<Severity> entity)
    {
        entity.ToTable("Severity", "RiskManagement");

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

        entity.Property(x => x.ConsequenceCategoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ConsequenceCategory)
            .WithMany(x => x.Severities)
            .HasForeignKey(x => x.ConsequenceCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SeverityValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.SeverityValue)
            .WithMany(x => x.Severities)
            .HasForeignKey(x => x.SeverityValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AffectedHistoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.AffectedHistory)
            .WithMany(x => x.Severities)
            .HasForeignKey(x => x.AffectedHistoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}