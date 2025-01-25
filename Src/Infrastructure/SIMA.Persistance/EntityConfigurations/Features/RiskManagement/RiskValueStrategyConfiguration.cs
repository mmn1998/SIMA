using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskValueStrategyConfiguration : IEntityTypeConfiguration<RiskValueStrategy>
{
    public void Configure(EntityTypeBuilder<RiskValueStrategy> entity)
    {
        entity.ToTable("RiskValueStrategy", "RiskManagement");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.RiskId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.Risk)
            .WithMany(x => x.RiskValueStrategies)
            .HasForeignKey(x => x.RiskId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.StrategyId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.Strategy)
            .WithMany(x => x.RiskValueStrategies)
            .HasForeignKey(x => x.StrategyId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}