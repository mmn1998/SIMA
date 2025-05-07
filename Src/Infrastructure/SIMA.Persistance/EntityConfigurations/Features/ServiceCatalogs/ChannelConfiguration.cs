using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ChannelConfiguration : IEntityTypeConfiguration<SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities.Channel>
{
    public void Configure(EntityTypeBuilder<SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities.Channel> entity)
    {
        entity.ToTable("Channel", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ChannelId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ServiceStatusId)
.HasConversion(v => v.Value, v => new ServiceStatusId(v));
        entity.HasOne(d => d.ServiceStatus).WithMany(p => p.Channels)
                .HasForeignKey(d => d.ServiceStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

