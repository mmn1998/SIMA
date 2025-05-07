using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class OwnershipTypeConfiguration : IEntityTypeConfiguration<OwnershipType>
{
    public void Configure(EntityTypeBuilder<OwnershipType> entity)
    {
        entity.ToTable("OwnershipType", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new OwnershipTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
    }
}