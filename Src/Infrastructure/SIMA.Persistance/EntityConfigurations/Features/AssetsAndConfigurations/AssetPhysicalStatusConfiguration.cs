using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.UserTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetPhysicalStatusConfiguration
{
    public void Configure(EntityTypeBuilder<AssetPhysicalStatus> entity)
    {
        entity.ToTable("AssetPhysicalStatus", "AssetAndConfiguration");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetPhysicalStatusId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
    }
}
public class AssetChangeOwnerHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetChangeOwnerHistory> entity)
    {
        entity.ToTable("AssetChangeOwnerHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetChangeOwnerHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetChangeOwnerHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.FromOwnerId)
          .HasConversion(
           v => v.Value,
           v => new StaffId(v));

        entity.HasOne(d => d.FromOwner)
            .WithMany(d => d.FromAssetChangeOwnerHistories)
            .HasForeignKey(d => d.FromOwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ToOwnerId)
          .HasConversion(
           v => v.Value,
           v => new StaffId(v));

        entity.HasOne(d => d.ToOwner)
            .WithMany(d => d.ToAssetChangeOwnerHistories)
            .HasForeignKey(d => d.ToOwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetChangePhysicalStatusHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetChangePhysicalStatusHistory> entity)
    {
        entity.ToTable("AssetChangePhysicalStatusHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetChangePhysicalStatusHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.FromAssetPhysicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetPhysicalStatusId(v));

        entity.HasOne(d => d.FromAssetPhysicalStatus)
            .WithMany(d => d.FromAssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.FromAssetPhysicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ToAssetPhysicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetPhysicalStatusId(v));

        entity.HasOne(d => d.ToAssetPhysicalStatus)
            .WithMany(d => d.ToAssetChangePhysicalStatusHistories)
            .HasForeignKey(d => d.ToAssetPhysicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetChangeTechnicalStatusHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetChangeTechnicalStatusHistory> entity)
    {
        entity.ToTable("AssetChangeTechnicalStatusHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetChangeTechnicalStatusHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.FromAssetTechnicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetTechnicalStatusId(v));

        entity.HasOne(d => d.FromTechnicalStatus)
            .WithMany(d => d.FromAssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.FromAssetTechnicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ToAssetTechnicalStatusId)
          .HasConversion(
           v => v.Value,
           v => new AssetTechnicalStatusId(v));

        entity.HasOne(d => d.ToTechnicalStatus)
            .WithMany(d => d.ToAssetChangeTechnicalStatusHistories)
            .HasForeignKey(d => d.ToAssetTechnicalStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetDocumentConfiguration
{
    public void Configure(EntityTypeBuilder<AssetDocument> entity)
    {
        entity.ToTable("AssetDocument", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetDocumentId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetDocuments)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.DocumentId)
          .HasConversion(
           v => v.Value,
           v => new DocumentId(v));

        entity.HasOne(d => d.Document)
            .WithMany(d => d.AssetDocuments)
            .HasForeignKey(d => d.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetIssueConfiguration
{
    public void Configure(EntityTypeBuilder<AssetIssue> entity)
    {
        entity.ToTable("AssetIssue", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetIssueId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetIssues)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.IssueId)
          .HasConversion(
           v => v.Value,
           v => new IssueId(v));

        entity.HasOne(d => d.Issue)
            .WithMany(d => d.AssetIssues)
            .HasForeignKey(d => d.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class AssetWarehouseHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<AssetWarehouseHistory> entity)
    {
        entity.ToTable("AssetWarehouseHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AssetWarehouseHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.IsCheckIn).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetWarehouseHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.WarehouseId)
          .HasConversion(
           v => v.Value,
           v => new WarehouseId(v));

        entity.HasOne(d => d.Warehouse)
            .WithMany(d => d.AssetWarehouseHistories)
            .HasForeignKey(d => d.WarehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
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
