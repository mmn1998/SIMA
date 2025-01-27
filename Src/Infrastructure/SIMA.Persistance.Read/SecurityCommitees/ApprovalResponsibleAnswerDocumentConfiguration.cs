using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class ApprovalResponsibleAnswerDocumentConfiguration : IEntityTypeConfiguration<ApprovalResponsibleAnswerDocument>
{
    public void Configure(EntityTypeBuilder<ApprovalResponsibleAnswerDocument> entity)
    {
        entity.ToTable("ApprovalResponsibleAnswerDocument", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ApprovalResponsibleAnswerDocumentId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, v => new DocumentId(v));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.ApprovalResponsibleAnswerDocuments)
            .HasForeignKey(x => x.DocumentId);
    }
}
