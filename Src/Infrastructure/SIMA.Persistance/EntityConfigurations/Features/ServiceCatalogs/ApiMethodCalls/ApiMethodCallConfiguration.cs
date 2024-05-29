using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.ApiMethodCalls;

public class ApiMethodCallConfiguration : IEntityTypeConfiguration<ApiMethodCall>
{
    public void Configure(EntityTypeBuilder<ApiMethodCall> entity)
    {
        entity.ToTable("ApiMethodCall", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new ApiMethodCallId(v))
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
