using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiDocumentConfiguration : IEntityTypeConfiguration<ApiDocument>
{
    public void Configure(EntityTypeBuilder<ApiDocument> entity)
    {
        entity.ToTable("ApiDocument", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiDocumentId(v))
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
            .WithMany(x=>x.ApiDocuments)
            .HasForeignKey(x=>x.ApiId);
        
        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x=>x.Document)
            .WithMany(x=>x.ApiDocuments)
            .HasForeignKey(x=>x.DocumentId);
    }
}
