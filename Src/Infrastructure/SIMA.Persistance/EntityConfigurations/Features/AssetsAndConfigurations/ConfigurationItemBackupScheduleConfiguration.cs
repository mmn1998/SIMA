using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemBackupScheduleConfiguration : IEntityTypeConfiguration<ConfigurationItemBackupSchedule>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemBackupSchedule> entity)
    {
        entity.ToTable("ConfigurationItemBackupSchedule", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemBackupSchedules)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DataCenterId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.DataCenter)
            .WithMany(d => d.ConfigurationItemBackupSchedules)
            .HasForeignKey(d => d.DataCenterId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BackupMethodId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.BackupMethod)
            .WithMany(d => d.ConfigurationItemBackupSchedules)
            .HasForeignKey(d => d.BackupMethodId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.TimeMeasurementId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.TimeMeasurement)
            .WithMany(d => d.ConfigurationItemBackupSchedules)
            .HasForeignKey(d => d.TimeMeasurementId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BackupConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.BackupConfigurationItem)
            .WithMany(d => d.BackupConfigurationItemBackupSchedules)
            .HasForeignKey(d => d.BackupConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}