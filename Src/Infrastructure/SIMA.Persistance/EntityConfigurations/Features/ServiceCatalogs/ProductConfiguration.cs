using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("Product", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ProductId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ServiceStatusId)
.HasConversion(v => v.Value, v => new ServiceStatusId(v));
        entity.HasOne(d => d.ServiceStatus).WithMany(p => p.Products)
                .HasForeignKey(d => d.ServiceStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ProviderCompanyId)
.HasConversion(v => v.Value, v => new CompanyId(v));
        entity.HasOne(d => d.ProviderCompany).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProviderCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

