using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ServiceCustomerTypeConfiguration : IEntityTypeConfiguration<ServiceCustomerType>
{
    public void Configure(EntityTypeBuilder<ServiceCustomerType> entity)
    {
        entity.ToTable("ServiceCustomerType", "ServiceCatalog");
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Id)
    .HasConversion(v => v.Value, v => new ServiceCustomerTypeId(v))
    .ValueGeneratedNever();
        entity.Property(x => x.ParentId)
    .HasConversion(v => v.Value, v => new ServiceCustomerTypeId(v));
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
              .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
    }
}
