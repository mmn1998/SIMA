using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> entity)
    {
        entity.ToTable("Warehouse", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new WarehouseId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);

        entity.Property(x => x.CompanyBuildingLocationId)
         .HasConversion(
          v => v.Value,
          v => new CompanyBuildingLocationId(v));

        entity.HasOne(d => d.CompanyBuildingLocation)
            .WithMany(d => d.Warehouses)
            .HasForeignKey(d => d.CompanyBuildingLocationId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
