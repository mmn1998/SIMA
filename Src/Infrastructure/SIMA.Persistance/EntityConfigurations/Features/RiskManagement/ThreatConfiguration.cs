using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public  class ThreatConfiguration : IEntityTypeConfiguration<Threat>
    {
        public void Configure(EntityTypeBuilder<Threat> entity)
        {
            entity.ToTable("Threat", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ThreatId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Risk).WithMany(p => p.Threats)
                .HasForeignKey(d => d.RiskId)
                .HasConstraintName("FK_Threat_Risk");

            entity.HasOne(d => d.ThreatType).WithMany(p => p.Threats)
                .HasForeignKey(d => d.ThreatTypeId)
                .HasConstraintName("FK_Threat_ThreatType");

            entity.HasOne(d => d.RiskPossibility).WithMany(p => p.Threats)
                .HasForeignKey(d => d.RiskPossibilityId)
                .HasConstraintName("FK_Threat_RiskPossibility");
        }
    }
}
