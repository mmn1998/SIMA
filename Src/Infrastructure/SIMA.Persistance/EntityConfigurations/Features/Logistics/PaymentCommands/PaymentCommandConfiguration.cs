using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.PaymentCommands;

public class PaymentCommandConfiguration : IEntityTypeConfiguration<PaymentCommand>
{
    public void Configure(EntityTypeBuilder<PaymentCommand> entity)
    {
        entity.ToTable("PaymentCommand", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new PaymentCommandId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.IsPrePayment).HasMaxLength(1).IsFixedLength();

        entity.Property(x => x.OrderingId)
            .HasConversion(x => x.Value, x => new OrderingId(x));
        entity.HasOne(x => x.Ordering)
            .WithMany(x => x.PaymentCommands)
            .HasForeignKey(x => x.OrderingId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
