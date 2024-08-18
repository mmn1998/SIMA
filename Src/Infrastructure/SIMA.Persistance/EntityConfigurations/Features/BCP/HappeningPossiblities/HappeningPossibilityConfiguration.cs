using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.HappeningPossiblities;

public class HappeningPossibilityConfiguration : IEntityTypeConfiguration<HappeningPossibility>
{
    public void Configure(EntityTypeBuilder<HappeningPossibility> entity)
    {
        entity.ToTable("HappeningPossibility", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new HappeningPossibilityId(v)).ValueGeneratedNever();
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
