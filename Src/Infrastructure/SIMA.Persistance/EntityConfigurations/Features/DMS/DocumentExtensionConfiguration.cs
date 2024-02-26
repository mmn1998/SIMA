using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.DMS;

public class DocumentExtensionConfiguration : IEntityTypeConfiguration<DocumentExtension>
{
    public void Configure(EntityTypeBuilder<DocumentExtension> entity)
    {
        entity.ToTable("DocumentExtension", "DMS");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new DocumentExtensionId(x));
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
