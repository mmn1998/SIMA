using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class MeetingScheduleConfiguration : IEntityTypeConfiguration<MeetingSchedule>
{
    public void Configure(EntityTypeBuilder<MeetingSchedule> entity)
    {
        entity.ToTable("MeetingSchedule", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingScheduleId(v));
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
            .WithMany(x => x.MeetingSchedules)
            .HasForeignKey(x => x.MeetingId);

        entity.Property(x => x.MeetingHoldingStatusId)
            .HasConversion(x => x.Value, v => new MeetingHoldingStatusId(v));
        entity.HasOne(x => x.MeetingHoldingStatus)
            .WithMany(x => x.MeetingSchedules)
            .HasForeignKey(x => x.MeetingHoldingStatusId);
    }
}
