using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemRelationshipConfiguration : IEntityTypeConfiguration<ConfigurationItemRelationship>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemRelationship> entity)
    {
        entity.ToTable("ConfigurationItemRelationship", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemRelationshipId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.ConfigurationItemRelationshipTypeId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemRelationshipTypeId(v));

        entity.HasOne(d => d.ConfigurationItemRelationshipType)
            .WithMany(d => d.ConfigurationItemRelationships)
            .HasForeignKey(d => d.ConfigurationItemRelationshipTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemRelationships)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RelatedConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.RelatedConfigurationItem)
            .WithMany(d => d.RelatedConfigurationItemRelationships)
            .HasForeignKey(d => d.RelatedConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
