using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemChangeStatusHistoryConfiguration : IEntityTypeConfiguration<ConfigurationItemChangeStatusHistory>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemChangeStatusHistory> entity)
    {
        entity.ToTable("ConfigurationItemChangeStatusHistory", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemChangeStatusHistoryId(v)).ValueGeneratedNever();
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
            .WithMany(d => d.ConfigurationItemChangeStatusHistories)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ToConfigurationItemStatusId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemStatusId(v));

        entity.HasOne(d => d.ToConfigurationItemStatus)
            .WithMany(d => d.ToConfigurationItemChangeStatusHistories)
            .HasForeignKey(d => d.ToConfigurationItemStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.FromConfigurationItemStatusId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemStatusId(v));

        entity.HasOne(d => d.FromConfigurationItemStatus)
            .WithMany(d => d.FromConfigurationItemChangeStatusHistories)
            .HasForeignKey(d => d.FromConfigurationItemStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
