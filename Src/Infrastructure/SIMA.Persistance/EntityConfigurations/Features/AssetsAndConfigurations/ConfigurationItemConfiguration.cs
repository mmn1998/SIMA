using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemConfiguration : IEntityTypeConfiguration<ConfigurationItem>
{
    public void Configure(EntityTypeBuilder<ConfigurationItem> entity)
    {
        entity.ToTable("ConfigurationItem", "AssetAndConfiguration");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.VersionNumber).HasMaxLength(20);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.CompanyBuildingLocationId)
          .HasConversion(
           v => v.Value,
           v => new LocationId(v));

        entity.HasOne(d => d.CompanyBuildingLocation)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.CompanyBuildingLocationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LicenseTypeId)
          .HasConversion(
           v => v.Value,
           v => new LicenseTypeId(v));

        entity.HasOne(d => d.LicenseType)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.LicenseTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConfigurationItemStatusId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemStatusId(v));

        entity.HasOne(d => d.ConfigurationItemStatus)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.ConfigurationItemStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConfigurationItemTypeId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemTypeId(v));

        entity.HasOne(d => d.ConfigurationItemType)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.ConfigurationItemTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LicenseSupplierId)
          .HasConversion(
           v => v.Value,
           v => new SupplierId(v));

        entity.HasOne(d => d.LicenseSupplier)
            .WithMany(d => d.LicensedConfigurationItems)
            .HasForeignKey(d => d.LicenseSupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SupplierId)
          .HasConversion(
           v => v.Value,
           v => new SupplierId(v));

        entity.HasOne(d => d.Supplier)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.OwnerId)
          .HasConversion(
           v => v.Value,
           v => new StaffId(v));

        entity.HasOne(d => d.Owner)
            .WithMany(d => d.ConfigurationItemOwners)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DataCenterId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.DataCenter)
            .WithMany(d => d.ConfigurationItems)
            .HasForeignKey(d => d.DataCenterId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
