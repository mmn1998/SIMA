using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class CobitScenarioConfiguration : IEntityTypeConfiguration<CobitScenario>
{
    public void Configure(EntityTypeBuilder<CobitScenario> entity)
    {
        entity.ToTable("CobitScenario", "RiskManagement");

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

        entity.Property(x => x.Name).HasMaxLength(200);

        entity.Property(x => x.CobitRiskCategoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.CobitRiskCategory)
            .WithMany(x => x.CobitScenarios)
            .HasForeignKey(x => x.CobitRiskCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}