using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class RiskDegreeConfiguration : IEntityTypeConfiguration<RiskDegree>
    {
        public void Configure(EntityTypeBuilder<RiskDegree> entity)
        {
            entity.ToTable("RiskDegree", "RiskManagement");

            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RiskDegreeId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Degree).IsRequired();
            entity.Property(e => e.Color).IsRequired().HasMaxLength(10);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
