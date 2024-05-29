using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.sanazConfiguration;

internal class ServiceCustomerConfiguration : IEntityTypeConfiguration<ServiceCustomer>
{
    public void Configure(EntityTypeBuilder<ServiceCustomer> entity)
    {
        entity.ToTable("ServiceCustomer", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceCustomerId(v))
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

        entity.HasOne(d => d.Service).WithMany(p => p.ServiceCustomers)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);



        entity.Property(x => x.ServiceCustomerTypeId)
         .HasConversion(v => v.Value, v => new ServiceCustomerTypeId(v));

        entity.HasOne(d => d.ServiceCustomerType).WithMany(p => p.ServiceCustomers)
                .HasForeignKey(d => d.ServiceCustomerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
