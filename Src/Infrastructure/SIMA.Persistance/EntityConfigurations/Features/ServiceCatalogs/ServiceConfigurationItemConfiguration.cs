using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ServiceConfigurationItemConfiguration : IEntityTypeConfiguration<ServiceConfigurationItem>
{
    public void Configure(EntityTypeBuilder<ServiceConfigurationItem> entity)
    {
        entity.ToTable("ServiceConfigurationItem", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceConfigurationItemId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ServiceId)
.HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceConfigurationItems)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConfigurationItemId)
.HasConversion(v => v.Value, v => new ConfigurationItemId(v));
        entity.HasOne(d => d.ConfigurationItem).WithMany(p => p.ServiceConfigurationItems)
                .HasForeignKey(d => d.ConfigurationItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

