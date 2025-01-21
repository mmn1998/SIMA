using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class TimeMeasurementConfiguration : IEntityTypeConfiguration<TimeMeasurement>
{
    public void Configure(EntityTypeBuilder<TimeMeasurement> entity)
    {
        entity.ToTable("TimeMeasurement", "Basic");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();

        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
    }
}