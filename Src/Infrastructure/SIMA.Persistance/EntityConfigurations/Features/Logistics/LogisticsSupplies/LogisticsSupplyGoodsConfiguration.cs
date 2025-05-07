using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyGoodsConfiguration : IEntityTypeConfiguration<LogisticsSupplyGoods>
{
    public void Configure(EntityTypeBuilder<LogisticsSupplyGoods> entity)
    {
        entity.ToTable("LogisticsSupplyGoods", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsSupplyGoodsId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(e => e.IsContractRequired).HasMaxLength(1).IsFixedLength();
        entity.Property(e => e.IsPrePaymentRequired).HasMaxLength(1).IsFixedLength();

        entity.Property(x => x.LogisticsRequestGoodsId)
            .HasConversion(x => x.Value, x => new LogisticsRequestGoodsId(x));
        entity.HasOne(x => x.LogisticsRequestGoods)
            .WithMany(x => x.LogisticsSupplyGoods)
            .HasForeignKey(x => x.LogisticsRequestGoodsId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsSupplyId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
        entity.HasOne(x => x.LogisticsSupply)
            .WithMany(x => x.LogisticsSupplyGoodses)
            .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class LogisticsSupplyDocumentConfiguration : IEntityTypeConfiguration<LogisticsSupplyDocument>
{
    public void Configure(EntityTypeBuilder<LogisticsSupplyDocument> entity)
    {
        entity.ToTable("LogisticsSupplyDocument", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsSupplyDocumentId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.LogisticsSupplyDocuments)
            .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.LogisticsSupplyId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyId(x));
        entity.HasOne(x => x.LogisticsSupply)
            .WithMany(x => x.LogisticsSupplyDocuments)
            .HasForeignKey(x => x.LogisticsSupplyId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
