using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsRequests;

public class LogisticsRequestConfiguration : IEntityTypeConfiguration<LogisticsRequest>
{
    public void Configure(EntityTypeBuilder<LogisticsRequest> entity)
    {
        entity.ToTable("LogisticsRequest", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsRequestId(v))
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

        entity.Property(x => x.IssueId)
            .HasConversion(x => x.Value, x => new IssueId(x));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.Logistics)
            .HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.RequesterId)
            .HasConversion(x => x.Value, x => new UserId(x));
        entity.HasOne(x => x.Requester)
            .WithMany(x => x.LogisticsRequests)
            .HasForeignKey(x => x.RequesterId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}