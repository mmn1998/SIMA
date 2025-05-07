using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Notifications.Messages
{
    public class MessageGroupDisplayConfiguration : IEntityTypeConfiguration<MessageGroupDisplay>
    {
        public void Configure(EntityTypeBuilder<MessageGroupDisplay> entity)
        {
            entity.ToTable("MessageGroupDisplay", "Notification");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new MessageGroupDisplayId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.MessageGroupDisplay)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.GroupId)
        .HasConversion
            (v => v.Value,
            v => new(v));
            entity.HasOne(x => x.Group)
                .WithMany(x => x.MessageGroupDisplay)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
