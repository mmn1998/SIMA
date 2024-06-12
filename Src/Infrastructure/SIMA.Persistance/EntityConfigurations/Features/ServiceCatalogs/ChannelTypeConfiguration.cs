using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ChannelTypeConfiguration : IEntityTypeConfiguration<ChannelType>
{
    public void Configure(EntityTypeBuilder<ChannelType> entity)
    {
        entity.ToTable("ChannelType", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ChannelTypeId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
              .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
    }
}
