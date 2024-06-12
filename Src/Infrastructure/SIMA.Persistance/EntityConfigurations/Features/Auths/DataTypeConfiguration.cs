using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.DataTypes.Entities;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
{
    public void Configure(EntityTypeBuilder<DataType> entity)
    {
        entity.ToTable("DataType", "Basic");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DataTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.IsMultiSelect).HasMaxLength(1);
        entity.Property(e => e.IsList).HasMaxLength(1);
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.HasIndex(e => e.Code).IsUnique();

        
    }
}
