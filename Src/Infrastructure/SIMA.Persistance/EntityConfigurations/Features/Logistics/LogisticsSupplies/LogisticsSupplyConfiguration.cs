using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyConfiguration : IEntityTypeConfiguration<LogisticsSupply>
{
    public void Configure(EntityTypeBuilder<LogisticsSupply> entity)
    {
        entity.ToTable("LogisticsSupply", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsSupplyId(v))
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
            .WithMany(x => x.LogisticsSupplies)
            .HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}