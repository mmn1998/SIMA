using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemCustomFieldConfiguration : IEntityTypeConfiguration<ConfigurationItemCustomField>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemCustomField> entity)
    {
        entity.ToTable("ConfigurationItemCustomField", "AssetAndConfiguration");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.Property(x => x.ParentId)
            .HasConversion(
             v => v.Value,
             v => new(v));
        entity.HasKey(i => i.Id);
        entity.Property(e => e.IsMandetory).HasMaxLength(1).IsFixedLength();
        entity.Property(e => e.BoundingViewName).HasMaxLength(200);
        entity.Property(e => e.ValueBoundingFeild).HasMaxLength(200);
        entity.Property(e => e.TextBoundingFeild).HasMaxLength(200);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);


        entity.Property(x => x.ConfigurationItemTypeId)
            .HasConversion(
             v => v.Value,
             v => new(v));
        entity.HasOne(x => x.ConfigurationItemType)
            .WithMany(x => x.ConfigurationItemCustomFields)
            .HasForeignKey(x => x.ConfigurationItemTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CustomeFieldTypeId)
            .HasConversion(
             v => v.Value,
             v => new(v));
        entity.HasOne(x => x.CustomeFieldType)
            .WithMany(x => x.ConfigurationItemCustomFields)
            .HasForeignKey(x => x.CustomeFieldTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}