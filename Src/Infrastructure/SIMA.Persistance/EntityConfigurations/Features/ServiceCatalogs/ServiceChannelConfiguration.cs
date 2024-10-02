using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ServiceChannelConfiguration : IEntityTypeConfiguration<ServiceChannel>
{
    public void Configure(EntityTypeBuilder<ServiceChannel> entity)
    {
        entity.ToTable("ServiceChanel", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceChannelId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ServiceId)
      .HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceChanneles)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.ChannelId)
      .HasConversion(v => v.Value, v => new ChannelId(v));
        entity.HasOne(d => d.Channel).WithMany(p => p.ServiceChannels)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

