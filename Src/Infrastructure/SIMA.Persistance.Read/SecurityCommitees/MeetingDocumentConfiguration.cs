using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class MeetingDocumentConfiguration : IEntityTypeConfiguration<MeetingDocument>
{
    public void Configure(EntityTypeBuilder<MeetingDocument> entity)
    {
        entity.ToTable("MeetingDocument", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingDocumentId(v));
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
            .WithMany(x => x.MeetingDocuments)
            .HasForeignKey(x => x.DocumentId);

        entity.Property(x => x.MeetingId)
            .HasConversion(x => x.Value, v => new MeetingId(v));
        entity.HasOne(x => x.Meeting)
            .WithMany(x => x.MeetingDocuments)
            .HasForeignKey(x => x.MeetingId);
    }
}
