using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiVersionConfiguration : IEntityTypeConfiguration<ApiVersion>
{
    public void Configure(EntityTypeBuilder<ApiVersion> entity)
    {
        entity.ToTable("ApiVersion", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiVersionId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.VersionNumber).HasMaxLength(20);
        entity.Property(x => x.IsCurrentVersion).HasMaxLength(1).IsUnicode(false);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ApiId)
            .HasConversion(x => x.Value, x => new ApiId(x));
        entity.HasOne(x=>x.Api)
            .WithMany(x=>x.ApiVersions)
            .HasForeignKey(x=>x.ApiId);
    }
}
