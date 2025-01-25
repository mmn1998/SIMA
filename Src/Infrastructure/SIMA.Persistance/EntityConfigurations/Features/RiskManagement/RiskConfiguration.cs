using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskConfiguration : IEntityTypeConfiguration<Risk>
{
    public void Configure(EntityTypeBuilder<Risk> entity)
    {
        entity.ToTable("Risk", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new RiskId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(2000);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.HasOne(d => d.RiskType).WithMany(p => p.Risks)
          .HasForeignKey(d => d.RiskTypeId)
            .HasConstraintName("FK_Risk_RiskType");

        entity.Property(x => x.ScenarioHistoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ScenarioHistory)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.ScenarioHistoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AffectedHistoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.AffectedHistory)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.AffectedHistoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.UseVulnerabilityId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.UseVulnerability)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.UseVulnerabilityId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConsequenceCategoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ConsequenceCategory)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.ConsequenceCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.TriggerStatusId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.TriggerStatus)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.TriggerStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.FrequencyId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.Frequency)
            .WithMany(x => x.Risks)
            .HasForeignKey(x => x.FrequencyId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
