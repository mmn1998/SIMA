using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ConfigurationTypeConfiguration : IEntityTypeConfiguration<ConfigurationType>
{
    public void Configure(EntityTypeBuilder<ConfigurationType> entity)
    {
        entity.ToTable("ConfigurationType", "Basic");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(x => x.IsList).HasMaxLength(1).IsFixedLength();
    }
}
