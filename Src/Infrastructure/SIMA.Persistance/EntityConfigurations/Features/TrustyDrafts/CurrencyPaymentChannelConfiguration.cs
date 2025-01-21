using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class CurrencyPaymentChannelConfiguration : IEntityTypeConfiguration<CurrencyPaymentChannel>
{
    public void Configure(EntityTypeBuilder<CurrencyPaymentChannel> entity)
    {
        entity.ToTable("CurrencyPaymentChannel", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
        entity.Property(x => x.Code).IsRequired().HasMaxLength(20);
        entity.HasIndex(x => x.Code).IsUnique();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.LocationId)
            .HasConversion(
             v => v.Value,
             v => new(v));

        entity.HasOne(x => x.Location)
            .WithMany(x => x.CurrencyPaymentChannels)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}