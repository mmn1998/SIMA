using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class SubjectMeetingConfiguration : IEntityTypeConfiguration<SubjectMeeting>
{
    public void Configure(EntityTypeBuilder<SubjectMeeting> entity)
    {
        entity.ToTable("SubjectMeeting", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new SubjectMeetingId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.SubjectId)
            .HasConversion(x => x.Value, v => new SubjectId(v));
        entity.HasOne(x => x.Subject)
            .WithMany(x => x.SubjectMeetings)
            .HasForeignKey(x => x.SubjectId);

        entity.Property(x => x.SubjectPriorityId)
            .HasConversion(x => x.Value, v => new SubjectPriorityId(v));
        entity.HasOne(x => x.SubjectPriority)
            .WithMany(x => x.SubjectMeetings)
            .HasForeignKey(x => x.SubjectPriorityId);

        entity.Property(x => x.MeetingId)
            .HasConversion(x => x.Value, v => new MeetingId(v));
        entity.HasOne(x => x.Meeting)
            .WithMany(x => x.SubjectMeetings)
            .HasForeignKey(x => x.MeetingId);

        entity.Property(x => x.ConfirmedBy)
            .HasConversion(x => x.Value, v => new UserId(v));
        entity.HasOne(x => x.Confirmer)
            .WithMany(x => x.SubjectMeetings)
            .HasForeignKey(x => x.ConfirmedBy);
    }
}
