using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class MatrixAValueConfiguration : IEntityTypeConfiguration<MatrixAValue>
{
    public void Configure(EntityTypeBuilder<MatrixAValue> entity)
    {
        entity.ToTable("MatrixAValue", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Color).IsRequired().HasMaxLength(10);
        entity.Property(e => e.ValueTitle).IsRequired().HasMaxLength(200);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}