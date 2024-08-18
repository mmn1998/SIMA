using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Scenarios
{
    public class ScenarioRecoveryCriteriaConfiguration : IEntityTypeConfiguration<ScenarioRecoveryCriteria>
    {
        public void Configure(EntityTypeBuilder<ScenarioRecoveryCriteria> entity)
        {
            entity.ToTable("ScenarioRecoveryCriteria", "BCP");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ScenarioRecoveryCriteriaId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Title).HasColumnType("nvarchar(MAX)").IsUnicode();
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
                .WithMany(x => x.ScenarioRecoveryCriterias)
                .HasForeignKey(x => x.ScenarioId);

        }
    }
}
