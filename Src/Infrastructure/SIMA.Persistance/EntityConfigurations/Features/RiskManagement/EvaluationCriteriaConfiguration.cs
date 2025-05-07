using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class EvaluationCriteriaConfiguration : IEntityTypeConfiguration<EvaluationCriteria>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteria> entity)
    {
        entity.ToTable("EvaluationCriteria", "RiskManagement");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new EvaluationCriteriaId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(x => x.RiskPossibilityId)
            .HasConversion(
            v => v.Value,
            v => new(v)
            );

        entity.HasOne(d => d.RiskPossibility).WithMany(p => p.EvaluationCriterias)
            .HasForeignKey(d => d.RiskPossibilityId)
            .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RiskImpactId)
            .HasConversion(
            v => v.Value,
            v => new(v)
            );

        entity.HasOne(d => d.RiskImpact).WithMany(p => p.EvaluationCriterias)
            .HasForeignKey(d => d.RiskImpactId)
            .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RiskDegreeId)
            .HasConversion(
            v => v.Value,
            v => new(v)
            );

        entity.HasOne(d => d.RiskDegree).WithMany(p => p.EvaluationCriterias)
            .HasForeignKey(d => d.RiskDegreeId)
            .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}
