using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class ApprovalSupervisorAnswerDocumentConfiguration : IEntityTypeConfiguration<ApprovalSupervisorAnswerDocument>
{
    public void Configure(EntityTypeBuilder<ApprovalSupervisorAnswerDocument> entity)
    {
        entity.ToTable("ApprovalSupervisorAnswerDocument", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ApprovalSupervisorAnswerDocumentId(v));
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
            .WithMany(x => x.ApprovalSupervisorAnswerDocuments)
            .HasForeignKey(x => x.DocumentId);
    }
}
