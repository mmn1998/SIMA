using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ApiAuthenticationMethodConfiguration : IEntityTypeConfiguration<ApiAuthenticationMethod>
{
    public void Configure(EntityTypeBuilder<ApiAuthenticationMethod> entity)
    {
        entity.ToTable("ApiAuthenticationMethod", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ApiAuthenticationMethodId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
    }
}

