using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ServiceApiConfiguration : IEntityTypeConfiguration<ServiceApi>
{
    public void Configure(EntityTypeBuilder<ServiceApi> entity)
    {
        entity.ToTable("ServiceApi", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ServiceApiId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ApiId)
            .HasConversion(x => x.Value, x => new ApiId(x));
        entity.HasOne(x=>x.Api)
            .WithMany(x=>x.ServiceApis)
            .HasForeignKey(x=>x.ApiId);
        
        entity.Property(x => x.ServiceId)
            .HasConversion(x => x.Value, x => new ServiceId(x));
        entity.HasOne(x=>x.Service)
            .WithMany(x=>x.ServiceApis)
            .HasForeignKey(x=>x.ServiceId);
    }
}
