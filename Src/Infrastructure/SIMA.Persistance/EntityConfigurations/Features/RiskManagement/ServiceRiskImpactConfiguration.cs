using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public  class ServiceRiskImpactConfiguration : IEntityTypeConfiguration<ServiceRiskImpact>
    {
        public void Configure(EntityTypeBuilder<ServiceRiskImpact> entity)
        {
            entity.ToTable("ServiceRiskImpact", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ServiceRiskImpactId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);


            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.ImpactScale).WithMany(p => p.ServiceRiskImpacts)
                .HasForeignKey(d => d.ImpactScaleId)
                .HasConstraintName("FK_ServiceRiskImpact_ImpactScale");

            entity.HasOne(d => d.RiskImpact).WithMany(p => p.ServiceRiskImpacts)
                .HasForeignKey(d => d.RiskImpactId)
                .HasConstraintName("FK_ServiceRiskImpact_ServiceRiskImpact");
        }
    }
}
