using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiRequestQueryStringParamConfiguration : IEntityTypeConfiguration<ApiRequestQueryStringParam>
{
    public void Configure(EntityTypeBuilder<ApiRequestQueryStringParam> entity)
    {
        entity.ToTable("ApiRequestQueryStringParam", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiRequestQueryStringParamId(v))
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
            .HasConversion(x => x.Value, x => new ApiRequestQueryStringParamId(x));
        
        entity.Property(x => x.ApiId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x=>x.Api)
            .WithMany(x=>x.ApiRequestQueryStringParams)
            .HasForeignKey(x=>x.ApiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
