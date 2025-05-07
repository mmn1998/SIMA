using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public  class ThreatTypeConfiguration : IEntityTypeConfiguration<ThreatType>
    {
        public void Configure(EntityTypeBuilder<ThreatType> entity)
        {
            entity.ToTable("ThreatType", "RiskManagement");


            entity.HasIndex(e => e.Code).IsUnique();

            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new ThreatTypeId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
