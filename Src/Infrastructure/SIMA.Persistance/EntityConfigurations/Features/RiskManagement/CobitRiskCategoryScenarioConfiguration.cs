using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class CobitRiskCategoryScenarioConfiguration : IEntityTypeConfiguration<CobitRiskCategoryScenario>
{
    public void Configure(EntityTypeBuilder<CobitRiskCategoryScenario> entity)
    {
        entity.ToTable("CobitRiskCategoryScenario", "RiskManagement");
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
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Risk)
            .WithMany(x => x.CobitRiskCategoryScenarios)
            .HasForeignKey(x => x.RiskId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CobitCategoryId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.CobitCategory)
            .WithMany(x => x.CobitRiskCategoryScenarios)
            .HasForeignKey(x => x.CobitCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CobitScenarioId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.CobitScenario)
            .WithMany(x => x.CobitRiskCategoryScenarios)
            .HasForeignKey(x => x.CobitScenarioId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
