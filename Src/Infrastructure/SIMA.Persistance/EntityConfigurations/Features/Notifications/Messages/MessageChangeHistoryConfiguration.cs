using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Notifications.Messages
{
    public class MessageChangeHistoryConfiguration : IEntityTypeConfiguration<MessageChangeHistory>
    {
        public void Configure(EntityTypeBuilder<MessageChangeHistory> entity)
        {
            entity.ToTable("MessageChangeHistory", "Notification");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new MessageChangeHistoryId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);

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
                .WithMany(x => x.MessageChangeHistories)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
