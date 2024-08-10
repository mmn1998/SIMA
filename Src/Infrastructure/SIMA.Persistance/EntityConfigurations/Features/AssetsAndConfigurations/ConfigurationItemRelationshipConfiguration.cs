using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemRelationshipConfiguration
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

        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.RelatedConfigurationItemRelationships)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RelatedConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.RelatedConfigurationItemVersioning)
            .WithMany(d => d.RelatedConfigurationItemRelationships)
            .HasForeignKey(d => d.RelatedConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
