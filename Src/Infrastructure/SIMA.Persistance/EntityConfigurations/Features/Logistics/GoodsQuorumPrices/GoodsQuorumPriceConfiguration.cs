using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsQuorumPrices
{
    public class GoodsQuorumPriceConfiguration : IEntityTypeConfiguration<GoodsQuorumPrice>
    {
        public void Configure(EntityTypeBuilder<GoodsQuorumPrice> entity)
        {
            entity.ToTable("GoodsQuorumPrice", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new GoodsQuorumPriceId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(e => e.Code)
                        .HasMaxLength(20).IsUnicode();
            entity.Property(e => e.Name)
                        .HasMaxLength(200).IsUnicode();
            entity.Property(e => e.IsRequiredBoardConfirmation).HasMaxLength(1);
            entity.Property(e => e.IsRequiredCeoConfirmation).HasMaxLength(1);
            entity.Property(e => e.IsRequiredSupplierWrittenInquiry).HasMaxLength(1);
        }
    }
}
