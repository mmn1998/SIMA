using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.TenderResults;


public class TenderResultConfiguration : IEntityTypeConfiguration<TenderResult>
{
    public void Configure(EntityTypeBuilder<TenderResult> entity)
    {
        entity.ToTable("TenderResult", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new TenderResultId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.TenderDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyDocumentId(x));
        entity.HasOne(x => x.TenderDocument)
            .WithMany(x => x.TenderResults)
            .HasForeignKey(x => x.TenderDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsSupplyId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
        entity.HasOne(x => x.LogisticsSupply)
            .WithMany(x => x.TenderResults)
            .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
