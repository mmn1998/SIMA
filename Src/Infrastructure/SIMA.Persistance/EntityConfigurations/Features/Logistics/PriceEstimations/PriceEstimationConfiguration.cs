using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.Entities;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.PriceEstimations
{
    public class PriceEstimationConfiguration : IEntityTypeConfiguration<PriceEstimation>
    {
        public void Configure(EntityTypeBuilder<PriceEstimation> entity)
        {
            entity.ToTable("PriceEstimation", "Logistics");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new PriceEstimationId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.EstimationPrice)
                        .HasColumnType("numeric(18,2)");

            entity.Property(x => x.LogisticsRequestId)
                .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
            entity.HasOne(x => x.LogisticsRequest)
                .WithMany(x => x.PriceEstimations)
                .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
