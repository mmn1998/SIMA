using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AccessManagement.AccessRequests;

public class AccessRequestDocumentConfiguration : IEntityTypeConfiguration<AccessRequestDocument>
{
    public void Configure(EntityTypeBuilder<AccessRequestDocument> entity)
    {
        entity.ToTable("AccessRequestDocument", "AccessManagement");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new AccessRequestDocumentId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.AccessRequestDocuments)
            .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AccessRequestId)
            .HasConversion(x => x.Value, x => new AccessRequestId(x));
        entity.HasOne(x => x.AccessRequest)
            .WithMany(x => x.AccessRequestDocuments)
            .HasForeignKey(x => x.AccessRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
