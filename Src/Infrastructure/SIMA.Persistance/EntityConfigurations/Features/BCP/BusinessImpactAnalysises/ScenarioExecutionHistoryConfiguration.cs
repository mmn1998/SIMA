using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class ScenarioExecutionHistoryConfiguration : IEntityTypeConfiguration<ScenarioExecutionHistory>
{
    public void Configure(EntityTypeBuilder<ScenarioExecutionHistory> entity)
    {
        entity.ToTable("ScenarioExecutionHistory", "BCP");
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
            .HasConversion
            (x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Scenario)
            .WithMany(x => x.ScenarioExecutionHistories)
            .HasForeignKey(x => x.ScenarioId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}