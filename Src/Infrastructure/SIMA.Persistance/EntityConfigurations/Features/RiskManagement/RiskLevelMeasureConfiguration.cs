using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public  class RiskLevelMeasureConfiguration : IEntityTypeConfiguration<RiskLevelMeasure>
    {
        public void Configure(EntityTypeBuilder<RiskLevelMeasure> entity)
        {
            entity.ToTable("RiskLevelMeasure", "RiskManagement");

            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RiskLevelMeasureId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.RiskLevel).WithMany(p => p.RiskLevelMeasures)
                .HasForeignKey(d => d.RiskLevelId)
                .HasConstraintName("FK_RiskLevelMeasure_RiskLevel");

            entity.HasOne(d => d.RiskPossibility).WithMany(p => p.RiskLevelMeasures)
                .HasForeignKey(d => d.RiskPossibilityId)
                .HasConstraintName("FK_RiskLevelMeasure_RiskPossibility");

            entity.HasOne(d => d.RiskImpact).WithMany(p => p.RiskLevelMeasures)
                .HasForeignKey(d => d.RiskImpactId)
                .HasConstraintName("FK_RiskLevelMeasure_RiskImpact");
        }
    }
}
