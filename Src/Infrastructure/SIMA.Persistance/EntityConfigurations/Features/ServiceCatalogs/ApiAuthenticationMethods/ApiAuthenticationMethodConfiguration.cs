using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.ApiAuthenticationMethods;

public class ApiAuthenticationMethodConfiguration : IEntityTypeConfiguration<ApiAuthentoicationMethod>
{
    public void Configure(EntityTypeBuilder<ApiAuthentoicationMethod> entity)
    {
        entity.ToTable("ApiAuthentoicationMethod", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new ApiAuthentoicationMethodId(v))
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
