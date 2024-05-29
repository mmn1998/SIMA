using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.ApiTypes;

public class ApiTypeConfiguration : IEntityTypeConfiguration<ApiType>
{
    public void Configure(EntityTypeBuilder<ApiType> entity)
    {
        entity.ToTable("ApiType", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new ApiTypeId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
    }
}
