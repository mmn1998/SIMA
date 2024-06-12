using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ServiceUserTypeConfiguration : IEntityTypeConfiguration<ServiceUserType>
{
    public void Configure(EntityTypeBuilder<ServiceUserType> entity)
    {
        entity.ToTable("ServiceUserType", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceUserTypeId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
              .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.ParentId)
         .HasConversion(v => v.Value, v => new ServiceUserTypeId(v));

    }
}
