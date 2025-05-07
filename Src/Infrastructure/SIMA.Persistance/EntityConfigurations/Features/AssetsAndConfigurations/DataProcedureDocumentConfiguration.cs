using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class DataProcedureDocumentConfiguration : IEntityTypeConfiguration<DataProcedureDocument>
{
    public void Configure(EntityTypeBuilder<DataProcedureDocument> entity)
    {
        entity.ToTable("DataProcedureDocument", "AssetAndConfiguration");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DataProcedureId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.DataProcedure)
            .WithMany(x => x.DataProcedureDocuments)
            .HasForeignKey(x => x.DataProcedureId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.DataProcedureDocuments)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}