using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class MeetingHoldingStatusConfiguration : IEntityTypeConfiguration<MeetingHoldingStatus>
{
    public void Configure(EntityTypeBuilder<MeetingHoldingStatus> entity)
    {
        entity.ToTable("MeetingHoldingStatus", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new MeetingHoldingStatusId(v));
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
