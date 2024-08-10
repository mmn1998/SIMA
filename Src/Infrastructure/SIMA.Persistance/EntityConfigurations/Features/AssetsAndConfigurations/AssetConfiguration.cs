using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.UserTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetConfiguration
{
    public void Configure(EntityTypeBuilder<Asset> entity)
    {
        entity.ToTable("Asset", "AssetAndConfiguration");

        
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.HasConfidentialInformation).HasMaxLength(1).IsFixedLength();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetPhysicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetPhysicalStatusId(v));

        entity.HasOne(d => d.AssetPhysicalStatus)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.AssetPhysicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.AssetTechnicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetTechnicalStatusId(v));

        entity.HasOne(d => d.AssetTechnicalStatus)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.AssetTechnicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetTypeId)
          .HasConversion(
           v => v.Value,
           v => new AssetTypeId(v));

        entity.HasOne(d => d.AssetType)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.AssetTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.BusinessCriticalityId)
          .HasConversion(
           v => v.Value,
           v => new BusinessCriticalityId(v));

        entity.HasOne(d => d.BusinessCriticality)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.BusinessCriticalityId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.OwnerId)
          .HasConversion(
           v => v.Value,
           v => new  StaffId(v));

        entity.HasOne(d => d.Owner)
            .WithMany(d => d.AssetOwners)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.OwnershipTypeId)
          .HasConversion(
           v => v.Value,
           v => new OwnershipTypeId(v));

        entity.HasOne(d => d.OwnershipType)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.OwnershipTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PhysicalLocationId)
          .HasConversion(
           v => v.Value,
           v => new LocationId(v));

        entity.HasOne(d => d.PhysicalLocation)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.PhysicalLocationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.SupplierId)
          .HasConversion(
           v => v.Value,
           v => new SupplierId(v));

        entity.HasOne(d => d.Supplier)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.UserTypeId)
          .HasConversion(
           v => v.Value,
           v => new  UserTypeId(v));

        entity.HasOne(d => d.UserType)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.UserTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.WarehouseId)
          .HasConversion(
           v => v.Value,
           v => new WarehouseId(v));

        entity.HasOne(d => d.Warehouse)
            .WithMany(d => d.Assets)
            .HasForeignKey(d => d.WarehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
