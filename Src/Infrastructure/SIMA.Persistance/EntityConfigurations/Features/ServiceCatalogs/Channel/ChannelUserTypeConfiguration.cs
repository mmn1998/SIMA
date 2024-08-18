using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Channel;

public class ChannelUserTypeConfiguration : IEntityTypeConfiguration<ChannelUserType>
{
    public void Configure(EntityTypeBuilder<ChannelUserType> entity)
    {
        entity.ToTable("ChannelUserType", "ServiceCatalog");

        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new ChannelUserTypeId(v))
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
            .WithMany(x => x.ChannelUserTypes)
            .HasForeignKey(x => x.ChannelId);

        entity.Property(x => x.UserTypeId)
            .HasConversion(x => x.Value, x => new UserTypeId(x));

        entity.HasOne(x => x.UserType)
            .WithMany(x => x.ChannelUserTypes)
            .HasForeignKey(x => x.UserTypeId);



        ;
    }
}
