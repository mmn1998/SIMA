using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.ValueObject;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class MeetingLabelConfiguration : IEntityTypeConfiguration<MeetingLabel>
{
    public void Configure(EntityTypeBuilder<MeetingLabel> entity)
    {
        entity.ToTable("MeetingLabel", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingLabelId(v));
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
            .WithMany(x => x.MeetingLabels)
            .HasForeignKey(x => x.MeetingId);

        entity.Property(x => x.LabelId)
            .HasConversion(x => x.Value, v => new LabelId(v));
        entity.HasOne(x => x.Label)
            .WithMany(x => x.MeetingLabels)
            .HasForeignKey(x => x.LabelId);
    }
}