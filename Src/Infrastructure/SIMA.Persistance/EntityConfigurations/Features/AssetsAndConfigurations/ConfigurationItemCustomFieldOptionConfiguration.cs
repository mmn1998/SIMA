using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemCustomFieldOptionConfiguration : IEntityTypeConfiguration<ConfigurationItemCustomFieldOption>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemCustomFieldOption> entity)
    {
        entity.ToTable("ConfigurationItemCustomFieldOption", "Asset");

        
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new ConfigurationItemCustomFieldOptionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.HasOne(f=>f.ConfigurationItem).WithMany(f=>f.ConfigurationItemCustomFieldOption).HasForeignKey(f=>f.ConfigurationItemId).OnDelete(DeleteBehavior.Restrict);
    }
}