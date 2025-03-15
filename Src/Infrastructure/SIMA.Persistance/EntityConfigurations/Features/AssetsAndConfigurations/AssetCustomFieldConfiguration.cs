using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetCustomFieldConfiguration : IEntityTypeConfiguration<AssetCustomField>
{
    public void Configure(EntityTypeBuilder<AssetCustomField> entity)
    {
        entity.ToTable("AssetCustomField", "AssetAndConfiguration");

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


        entity.Property(x => x.AssetTypeId)
            .HasConversion(
             v => v.Value,
             v => new(v));
        entity.HasOne(x => x.AssetType)
            .WithMany(x => x.AssetCustomFields)
            .HasForeignKey(x => x.AssetTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CustomeFieldTypeId)
            .HasConversion(
             v => v.Value,
             v => new(v));
        entity.HasOne(x => x.CustomeFieldType)
            .WithMany(x => x.AssetCustomFields)
            .HasForeignKey(x => x.CustomeFieldTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
