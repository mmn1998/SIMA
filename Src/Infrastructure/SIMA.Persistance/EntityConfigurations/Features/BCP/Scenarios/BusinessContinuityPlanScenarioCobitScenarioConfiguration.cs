using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Scenarios;

public class BusinessContinuityPlanScenarioCobitScenarioConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanScenarioCobitScenario>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityPlanScenarioCobitScenario> entity)
    {
        entity.ToTable("BusinessContinuityPlanScenarioCobitScenario", "BCP");
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

        entity.Property(x => x.ScenarioId)
      .HasConversion(
      x => x.Value,
      x => new ScenarioId(x)
      );
        entity.HasOne(x => x.Scenario)
            .WithMany(x => x.BusinessContinuityPlanScenarioCobitScenarios)
            .HasForeignKey(x => x.ScenarioId);

        entity.Property(x => x.CobitScenarioId)
      .HasConversion(
      x => x.Value,
      x => new(x)
      );
        entity.HasOne(x => x.CobitScenario)
            .WithMany(x => x.BusinessContinuityPlanScenarioCobitScenarios)
            .HasForeignKey(x => x.CobitScenarioId);

    }
}
