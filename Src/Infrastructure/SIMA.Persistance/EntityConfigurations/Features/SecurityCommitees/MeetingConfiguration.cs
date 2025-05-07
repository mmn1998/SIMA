using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> entity)
    {
        entity.ToTable("Meeting", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Description).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();

        entity.Property(x => x.IssueId)
    .HasConversion(x => x.Value, v => new IssueId(v));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.Meetings)
            .HasForeignKey(x => x.IssueId);
    }
}
