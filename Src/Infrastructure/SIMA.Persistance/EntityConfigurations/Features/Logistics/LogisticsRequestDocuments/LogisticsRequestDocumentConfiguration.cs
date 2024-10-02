using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsRequestDocuments
{
    public class LogisticsRequestDocumentConfiguration : IEntityTypeConfiguration<LogisticsRequestDocument>
    {
        public void Configure(EntityTypeBuilder<LogisticsRequestDocument> entity)
        {
            entity.ToTable("LogisticsRequestDocument", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new LogisticsRequestDocumentId(v))
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
                .WithMany(x => x.LogisticsRequestDocuments)
                .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.LogisticsRequestId)
                .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
            entity.HasOne(x => x.LogisticsRequest)
                .WithMany(x => x.LogisticsRequestDocuments)
                .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
