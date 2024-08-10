using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class LicenseTypeConfiguration
{
    public void Configure(EntityTypeBuilder<LicenseType> entity)
    {
        entity.ToTable("LicenseType", "AssetAndConfiguration");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new LicenseTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
    }
}
public class ConfigurationItemAccessInfoConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemAccessInfo> entity)
    {
        entity.ToTable("ConfigurationItemAccessInfo", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemAccessInfoId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemAccessInfos)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}public class ConfigurationItemAssetConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemAsset> entity)
    {
        entity.ToTable("ConfigurationItemAsset", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemAssetId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemAssets)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.ConfigurationItemAssets)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class ConfigurationItemAssetHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemAssetHistory> entity)
    {
        entity.ToTable("ConfigurationItemAssetHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemAssetHistoryId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.IsAssigned).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemAssetHistories)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new AssetId(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.ConfigurationItemAssetHistories)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}public class ConfigurationItemDocumentConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemDocument> entity)
    {
        entity.ToTable("ConfigurationItemDocument", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemDocumentId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemDocuments)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DocumentId)
          .HasConversion(
           v => v.Value,
           v => new DocumentId(v));

        entity.HasOne(d => d.Document)
            .WithMany(d => d.ConfigurationItemDocuments)
            .HasForeignKey(d => d.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}public class ConfigurationItemIssueConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemIssue> entity)
    {
        entity.ToTable("ConfigurationItemIssue", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemIssueId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemIssues)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IssueId)
          .HasConversion(
           v => v.Value,
           v => new IssueId(v));

        entity.HasOne(d => d.Issue)
            .WithMany(d => d.ConfigurationItemIssues)
            .HasForeignKey(d => d.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}public class ConfigurationItemChangeOwnerHistoryConfiguration
{
    public void Configure(EntityTypeBuilder<ConfigurationItemChangeOwnerHistory> entity)
    {
        entity.ToTable("ConfigurationItemChangeOwnerHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemChangeOwnerHistoryId(v)).ValueGeneratedNever();
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
           v => new ConfigurationItemId(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemChangeOwnerHistories)
            .HasForeignKey(d => d.ConfigurationItem)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ToOwnerId)
          .HasConversion(
           v => v.Value,
           v => new StaffId(v));

        entity.HasOne(d => d.ToOwner)
            .WithMany(d => d.ToConfigurationItemChangeOwnerHistories)
            .HasForeignKey(d => d.ToOwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.FromOwnerId)
          .HasConversion(
           v => v.Value,
           v => new StaffId(v));

        entity.HasOne(d => d.FromOwner)
            .WithMany(d => d.FromConfigurationItemChangeOwnerHistories)
            .HasForeignKey(d => d.FromOwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}