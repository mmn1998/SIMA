using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Notifications.Messages
{
    public class MessagePositionDisplayConfiguration : IEntityTypeConfiguration<MessagePositionDisplay>
    {
        public void Configure(EntityTypeBuilder<MessagePositionDisplay> entity)
        {
            entity.ToTable("MessagePositionDisplay", "Notification");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new MessagePositionDisplayId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.MessagePositionDisplay)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.PositionId)
        .HasConversion
            (v => v.Value,
            v => new(v));
            entity.HasOne(x => x.Position)
                .WithMany(x => x.MessagePositionDisplay)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
