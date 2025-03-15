using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiResponseHeaderParamConfiguration : IEntityTypeConfiguration<ApiResponseHeaderParam>
{
    public void Configure(EntityTypeBuilder<ApiResponseHeaderParam> entity)
    {
        entity.ToTable("ApiResponseHeaderParam", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiResponseHeaderParamId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Name).HasMaxLength(200);
        entity.Property(x => x.DataType).HasMaxLength(200);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ParentId)
            .HasConversion(x => x.Value, x => new ApiResponseHeaderParamId(x));
        
        entity.Property(x => x.ApiId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x=>x.Api)
            .WithMany(x=>x.ApiResponseHeaderParams)
            .HasForeignKey(x=>x.ApiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
