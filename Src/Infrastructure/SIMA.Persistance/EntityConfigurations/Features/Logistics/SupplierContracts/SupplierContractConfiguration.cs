using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.SupplierContracts;

public class SupplierContractConfiguration : IEntityTypeConfiguration<SupplierContract>
{
    public void Configure(EntityTypeBuilder<SupplierContract> entity)
    {
        entity.ToTable("SupplierContract", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new SupplierContractId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ContractDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsSupplyDocumentId(x));
        entity.HasOne(x => x.ContractDocument)
            .WithMany(x => x.SupplierContracts)
            .HasForeignKey(x => x.ContractDocumentId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.CandidatedSupplierId)
            .HasConversion(x => x.Value, x => new CandidatedSupplierId(x));
        entity.HasOne(x => x.CandidatedSupplier)
            .WithMany(x => x.SupplierContracts)
            .HasForeignKey(x => x.CandidatedSupplierId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}