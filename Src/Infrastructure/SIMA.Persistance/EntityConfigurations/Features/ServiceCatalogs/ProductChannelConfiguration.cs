using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ProductChannelConfiguration : IEntityTypeConfiguration<ProductChannel>
{
    public void Configure(EntityTypeBuilder<ProductChannel> entity)
    {
        entity.ToTable("ProductChannel", "ServiceCatalog");

        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new ProductChannelId(v))
            .ValueGeneratedNever();

        entity.HasKey(e => e.Id);

        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ChannelId)
            .HasConversion(x => x.Value, x => new ChannelId(x));

        entity.HasOne(x => x.Channel)
            .WithMany(x => x.ProductChannels)
            .HasForeignKey(x => x.ChannelId);
     
        entity.Property(x => x.ProductId)
            .HasConversion(x => x.Value, x => new ProductId(x));

        entity.HasOne(x => x.Product)
            .WithMany(x => x.ProductChannels)
            .HasForeignKey(x => x.ProductId);



        ;
    }
}
