using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class RiskConfiguration : IEntityTypeConfiguration<Risk>
    {
        public void Configure(EntityTypeBuilder<Risk> entity)
        {
            entity.ToTable("Risk", "RiskManagement");

            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RiskId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(2000);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.RiskType).WithMany(p => p.Risks)
              .HasForeignKey(d => d.RiskTypeId)
                .HasConstraintName("FK_Risk_RiskType");
        }
    }
}
