using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Channel;

public class ChannelAccessPointConfiguration : IEntityTypeConfiguration<ChannelAccessPoint>
{
    public void Configure(EntityTypeBuilder<ChannelAccessPoint> entity)
    {
        entity.ToTable("ChannelAccessPoint", "ServiceCatalog");

        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new ChannelAccessPointId(v))
            .ValueGeneratedNever();

        entity.HasKey(e => e.Id);

        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");

        entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime");

        entity.Property(e => e.IpAddress).HasMaxLength(20).IsUnicode().HasColumnType("nvarchar"); 

        entity.Property(e => e.Port).HasMaxLength(5).IsUnicode().HasColumnType("nvarchar");

        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ChannelId)
            .HasConversion(x => x.Value, x => new ChannelId(x));

        entity.HasOne(x => x.Channel)
            .WithMany(x => x.ChannelAccessPoints)
            .HasForeignKey(x => x.ChannelId);

     

        ;
    }
}
