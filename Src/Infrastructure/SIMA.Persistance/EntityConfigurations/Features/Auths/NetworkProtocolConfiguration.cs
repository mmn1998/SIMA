using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.Entities;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class NetworkProtocolConfiguration : IEntityTypeConfiguration<NetworkProtocol>
{
    public void Configure(EntityTypeBuilder<NetworkProtocol> entity)
    {
        entity.ToTable("NetworkProtocol", "Basic");
        entity.Property(x => x.Id)
    .HasConversion(v => v.Value, v => new NetworkProtocolId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
              .HasMaxLength(20);
        entity.Property(e => e.Name)
              .HasMaxLength(200).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
    }
}
