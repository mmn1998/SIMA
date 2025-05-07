using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemApiConfiguration : IEntityTypeConfiguration<ConfigurationItemApi>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemApi> entity)
    {
        entity.ToTable("ConfigurationItemApi", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemApiId(v)).ValueGeneratedNever();
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
            .WithMany(d => d.ConfigurationItemApis)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ApiId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.Api)
            .WithMany(d => d.ConfigurationItemApis)
            .HasForeignKey(d => d.ApiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}