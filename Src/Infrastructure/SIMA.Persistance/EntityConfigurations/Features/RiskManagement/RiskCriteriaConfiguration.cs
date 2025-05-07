using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class RiskCriteriaConfiguration : IEntityTypeConfiguration<RiskCriteria>
    {
        public void Configure(EntityTypeBuilder<RiskCriteria> entity)
        {
            entity.ToTable("RiskCriteria", "RiskManagement");

            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RiskCriteriaId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.RiskDegree).WithMany(p => p.RiskCriterias)
                .HasForeignKey(d => d.RiskDegreeId)
                .HasConstraintName("FK_RiskCriteria_RiskDegree");

            entity.HasOne(d => d.RiskPossibility).WithMany(p => p.RiskCriterias)
                 .HasForeignKey(d => d.RiskPossibilityId)
                 .HasConstraintName("FK_RiskCriteria_RiskPossibility");

            entity.HasOne(d => d.RiskImpact).WithMany(p => p.RiskCriterias)
                 .HasForeignKey(d => d.RiskImpactId)
                 .HasConstraintName("FK_RiskCriteria_RiskImpact");
        }
    }
}
