using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemTypeConfiguration : IEntityTypeConfiguration<ConfigurationItemType>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemType> entity)
    {
        entity.ToTable("ConfigurationItemType", "AssetAndConfiguration");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemTypeId(v)).ValueGeneratedNever();
        entity.Property(x => x.ParentId)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemTypeId(v));
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