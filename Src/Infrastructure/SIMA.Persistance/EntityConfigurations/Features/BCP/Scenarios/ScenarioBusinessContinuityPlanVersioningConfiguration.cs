using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanAssumptions.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioBusinessContinuityPlanVersionings.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Scenarios
{
    public class ScenarioBusinessContinuityPlanVersioningConfiguration : IEntityTypeConfiguration<ScenarioBusinessContinuityPlanVersioning>
    {
        public void Configure(EntityTypeBuilder<ScenarioBusinessContinuityPlanVersioning> entity)
        {
            entity.ToTable(" ScenarioBusinessContinuityPlanVersioning", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ScenarioBusinessContinuityPlanVersioningId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.ScenarioBusinessContinuityPlanVersionings)
                .HasForeignKey(x => x.ScenarioId);

            entity.Property(x => x.BusinessContinuityPlanVersioningId)
          .HasConversion(
          x => x.Value,
          x => new BusinessContinuityPlanVersioningId(x)
          );
            entity.HasOne(x => x.BusinessContinuityPlanVersioning)
                .WithMany(x => x.ScenarioBusinessContinuityPlanVersionings)
                .HasForeignKey(x => x.BusinessContinuityPlanVersioningId);

        }
    }
}

