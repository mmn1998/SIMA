using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskLevelConfiguration : IEntityTypeConfiguration<RiskLevel>
{
    public void Configure(EntityTypeBuilder<RiskLevel> entity)
    {
        entity.ToTable("RiskLevel", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new RiskLevelId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.RiskValueId)
      .HasConversion(v => v.Value, v => new RiskValueId(v));
        entity.HasOne(d => d.RiskValue).WithMany(p => p.RiskLevels)
        .HasForeignKey(d => d.RiskValueId)
        .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.SeverityValueId)
      .HasConversion(v => v.Value, v => new SeverityValueId(v));
        entity.HasOne(d => d.SeverityValue).WithMany(p => p.RiskLevels)
        .HasForeignKey(d => d.SeverityValueId)
        .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.CurrentOccurrenceProbabilityValueId)
      .HasConversion(v => v.Value, v => new CurrentOccurrenceProbabilityValueId(v));
        entity.HasOne(d => d.CurrentOccurrenceProbabilityValue).WithMany(p => p.RiskLevels)
        .HasForeignKey(d => d.CurrentOccurrenceProbabilityValueId)
        .OnDelete(DeleteBehavior.ClientSetNull);




    }
}
