using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.ImportanceDegrees;

public class ImportanceDegreeConfiguration : IEntityTypeConfiguration<ImportanceDegree>
{
    public void Configure(EntityTypeBuilder<ImportanceDegree> entity)
    {
        entity.ToTable("ImportanceDegree", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ImportanceDegreeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
