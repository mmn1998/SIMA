using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.ValueObjects;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AccessManagement.AccessRequests;

public class AccessRequestConfiguration : IEntityTypeConfiguration<AccessRequest>
{
    public void Configure(EntityTypeBuilder<AccessRequest> entity)
    {
        entity.ToTable("AccessRequest", "AccessManagement");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new AccessRequestId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.HasAutoRenew).HasMaxLength(1).IsFixedLength();

        entity.Property(e => e.PortDestinationFrom).HasMaxLength(5);
        entity.Property(e => e.PortDestinationTo).HasMaxLength(5);

        entity.Property(e => e.IpDestinationFrom).HasMaxLength(20);
        entity.Property(e => e.IpDestinationTo).HasMaxLength(20);
        entity.Property(e => e.IpSourceFrom).HasMaxLength(20);
        entity.Property(e => e.IpSourceTo).HasMaxLength(20);

        entity.Property(x => x.IssueId)
            .HasConversion(x => x.Value, x => new IssueId(x));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.AccessRequests)
            .HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.AccessTypeId)
            .HasConversion(x => x.Value, x => new AccessTypeId(x));
        entity.HasOne(x => x.AccessType)
            .WithMany(x => x.AccessRequests)
            .HasForeignKey(x => x.AccessTypeId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.NetworkProtocolId)
            .HasConversion(x => x.Value, x => new NetworkProtocolId(x));
        entity.HasOne(x => x.NetworkProtocol)
            .WithMany(x => x.AccessRequests)
            .HasForeignKey(x => x.NetworkProtocolId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}