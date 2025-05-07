using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetChangeOwnerHistoryConfiguration : IEntityTypeConfiguration<AssetChangeOwnerHistory>
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
