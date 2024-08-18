using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemChangeOwnerHistoryConfiguration : IEntityTypeConfiguration<ConfigurationItemChangeOwnerHistory>
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
            .HasForeignKey(d => d.ConfigurationItemId)
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