using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskTypeConfiguration : IEntityTypeConfiguration<RiskType>
{
    public void Configure(EntityTypeBuilder<RiskType> entity)
    {
        entity.ToTable("RiskType", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new RiskTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}