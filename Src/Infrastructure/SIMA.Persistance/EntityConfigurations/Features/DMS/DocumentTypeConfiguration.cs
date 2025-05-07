using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.DMS;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> entity)
    {
        entity.ToTable("DocumentType", "DMS");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new DocumentTypeId(x));
        entity.HasKey(x => x.Id);
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
    }
}
