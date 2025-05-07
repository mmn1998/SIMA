using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierDocuments
{
    public class SupplierDocumentConfiguration : IEntityTypeConfiguration<SupplierDocument>
    {
        public void Configure(EntityTypeBuilder<SupplierDocument> entity)
        {
            entity.ToTable("SupplierDocument", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierDocumentId(v))
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
                .WithMany(x => x.SupplierDocuments)
                .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierDocuments)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}
