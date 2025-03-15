using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemAccessInfoConfiguration : IEntityTypeConfiguration<ConfigurationItemAccessInfo>
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

        entity.Property(x => x.ConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemAccessInfos)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}