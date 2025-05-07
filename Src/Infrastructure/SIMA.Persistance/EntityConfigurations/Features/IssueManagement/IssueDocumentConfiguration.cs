using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

public class IssueDocumentConfiguration : IEntityTypeConfiguration<IssueDocument>
{
    public void Configure(EntityTypeBuilder<IssueDocument> entity)
    {
        entity.ToTable("IssueDocument", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueDocumentId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.IssueId)
               .HasConversion(
                v => v.Value,
                v => new IssueId(v));
        entity.Property(x => x.DocumentId)
               .HasConversion(
                v => v.Value,
                v => new DocumentId(v));
        entity.HasOne(d => d.Issue).WithMany(p => p.IssueDocuments)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(d => d.Document).WithMany(p => p.IssueDocuments)
                .HasForeignKey(d => d.DocumentId);
    }
}
