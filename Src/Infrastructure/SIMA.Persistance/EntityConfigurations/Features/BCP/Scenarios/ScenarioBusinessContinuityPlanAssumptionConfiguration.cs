using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Scenarios
{
    public class ScenarioBusinessContinuityPlanAssumptionConfiguration : IEntityTypeConfiguration<ScenarioBusinessContinuityPlanAssumption>
    {
        public void Configure(EntityTypeBuilder<ScenarioBusinessContinuityPlanAssumption> entity)
        {
            entity.ToTable("ScenarioBusinessContinuityPlanAssumption", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ScenarioBusinessContinuityPlanAssumptionId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.ScenarioBusinessContinuityPlanAssumptions)
                .HasForeignKey(x => x.ScenarioId);

            entity.Property(x => x.BusinessContinuityPlanAssumptionId)
          .HasConversion(
          x => x.Value,
          x => new BusinessContinuityPlanAssumptionId(x)
          );
            entity.HasOne(x => x.BusinessContinuityPlanAssumption)
                .WithMany(x => x.ScenarioBusinessContinuityPlanAssumptions)
                .HasForeignKey(x => x.BusinessContinuityPlanAssumptionId);

        }
    }
}
