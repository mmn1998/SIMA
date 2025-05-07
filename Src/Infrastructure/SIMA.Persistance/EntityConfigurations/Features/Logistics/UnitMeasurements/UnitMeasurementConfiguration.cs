using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.UnitMeasurements
{
    public class UnitMeasurementConfiguration : IEntityTypeConfiguration<UnitMeasurement>
    {
        public void Configure(EntityTypeBuilder<UnitMeasurement> entity)
        {
            entity.ToTable("UnitMeasurement", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new UnitMeasurementId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.Code)
                        .HasMaxLength(20).IsUnicode();
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.IsRequireItConfirmation).HasMaxLength(1);
        }
    }
}
