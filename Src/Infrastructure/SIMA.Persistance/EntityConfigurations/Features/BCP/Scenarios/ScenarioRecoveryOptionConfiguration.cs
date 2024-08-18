using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryOptions.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Scenarios
{
    public class ScenarioRecoveryOptionConfiguration : IEntityTypeConfiguration<ScenarioRecoveryOption>
    {
        public void Configure(EntityTypeBuilder<ScenarioRecoveryOption> entity)
        {
            entity.ToTable("ScenarioRecoveryOption", "BCP");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ScenarioRecoveryOptionId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.ScenarioRecoveryOptions)
                .HasForeignKey(x => x.ScenarioId);

        }
    }
}
