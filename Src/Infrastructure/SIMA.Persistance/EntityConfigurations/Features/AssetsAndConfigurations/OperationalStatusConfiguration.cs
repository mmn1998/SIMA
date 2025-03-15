using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class OperationalStatusConfiguration : IEntityTypeConfiguration<OperationalStatus>
{
    public void Configure(EntityTypeBuilder<OperationalStatus> entity)
    {   
        entity.ToTable("OperationalStatus", "Asset");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
    }
}