using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiRequestHeaderParamConfiguration : IEntityTypeConfiguration<ApiRequestHeaderParam>
{
    public void Configure(EntityTypeBuilder<ApiRequestHeaderParam> entity)
    {
        entity.ToTable("ApiRequestHeaderParam", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiRequestHeaderParamId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Name).HasMaxLength(200);
        entity.Property(x => x.DataType).HasMaxLength(200);
        entity.Property(x => x.IsMandatory).HasMaxLength(1).IsUnicode(false);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ParentId)
            .HasConversion(x => x.Value, x => new ApiRequestHeaderParamId(x));
        
        entity.Property(x => x.ApiVersionId)
            .HasConversion(x => x.Value, x => new ApiVersionId(x));
        entity.HasOne(x=>x.ApiVersion)
            .WithMany(x=>x.ApiRequestHeaderParams)
            .HasForeignKey(x=>x.ApiVersionId);
    }
}
