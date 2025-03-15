using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.Back_UpPeriods;

public class BackupPeriodConfiguration : IEntityTypeConfiguration<BackupPeriod>
{
    public void Configure(EntityTypeBuilder<BackupPeriod> entity)
    {
        entity.ToTable("BackupPeriod", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BackupPeriodId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}