using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class CustomeFieldTypeConfiguration : IEntityTypeConfiguration<CustomeFieldType>
{
    public void Configure(EntityTypeBuilder<CustomeFieldType> entity)
    {
        entity.ToTable("CustomeFieldType", "Basic");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new CustomeFieldTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(x => x.IsList).HasMaxLength(1).IsFixedLength();
        entity.Property(x => x.IsMultiSelect).HasMaxLength(1).IsFixedLength();
    }
}
