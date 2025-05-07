using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class MeetingReasonConfiguration : IEntityTypeConfiguration<MeetingReason>
{
    public void Configure(EntityTypeBuilder<MeetingReason> entity)
    {
        entity.ToTable("MeetingReason", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingReasonId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.MeetingId)
            .HasConversion(x => x.Value, v => new MeetingId(v));
        entity.HasOne(x => x.Meeting)
            .WithMany(x => x.MeetingReasons)
            .HasForeignKey(x => x.MeetingId);

        entity.Property(x => x.MeetingHoldingReasonId)
            .HasConversion(x => x.Value, v => new MeetingHoldingReasonId(v));
        entity.HasOne(x => x.MeetingHoldingReason)
            .WithMany(x => x.meetingReasons)
            .HasForeignKey(x => x.MeetingHoldingReasonId);
    }
}
