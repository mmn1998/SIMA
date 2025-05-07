using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class MeetingHoldingReasonConfiguration : IEntityTypeConfiguration<MeetingHoldingReason>
{
    public void Configure(EntityTypeBuilder<MeetingHoldingReason> entity)
    {
        entity.ToTable("MeetingHoldingReason", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingHoldingReasonId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
    }
}
