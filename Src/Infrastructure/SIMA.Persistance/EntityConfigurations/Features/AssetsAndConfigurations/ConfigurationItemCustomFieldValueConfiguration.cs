using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemCustomFieldValueConfiguration : IEntityTypeConfiguration<ConfigurationItemCustomFieldValue>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemCustomFieldValue> entity)
    {
        entity.ToTable("ConfigurationItemCustomFieldValue", "AssetAndConfiguration");
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
            .WithMany(d => d.ConfigurationItemCustomFieldValues)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AssetId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.Asset)
            .WithMany(d => d.ConfigurationItemCustomFieldValues)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
