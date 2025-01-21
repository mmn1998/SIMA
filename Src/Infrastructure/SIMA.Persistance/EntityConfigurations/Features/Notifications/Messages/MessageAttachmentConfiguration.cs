using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Notifications.Messages
{
    public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
    {
        public void Configure(EntityTypeBuilder<MessageAttachment> entity)
        {
            entity.ToTable("MessageAttachment", "Notification");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new MessageAttachmentId(v)).ValueGeneratedNever();
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
                .WithMany(x => x.MessageAttachments)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(e => e.DocumentId)
        .HasConversion
            (v => v.Value,
            v => new(v));
            entity.HasOne(x => x.Document)
                .WithMany(x => x.MessageAttachments)
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
