using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Notifications.Messages
{
    public class MessageSeenStatisticsConfiguration : IEntityTypeConfiguration<MessageSeenStatistics>
    {
        public void Configure(EntityTypeBuilder<MessageSeenStatistics> entity)
        {
            entity.ToTable("MessageSeenStatistics", "Notification");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new MessageSeenStatisticsId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);


            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(e => e.MessageId)
          .HasConversion
              (v => v.Value,
              v => new(v));
            entity.HasOne(x => x.Message)
                .WithMany(x => x.MessageSeenStatisticses)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.StaffId)
        .HasConversion
            (v => v.Value,
            v => new(v));
            entity.HasOne(x => x.Staff)
                .WithMany(x => x.MessageSeenStatisticses)
                .HasForeignKey(x => x.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
