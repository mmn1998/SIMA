using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.SolutionPeriorities;

public class SolutionPeriorityConfiguration : IEntityTypeConfiguration<SolutionPriority>
{
    public void Configure(EntityTypeBuilder<SolutionPriority> entity)
    {
        entity.ToTable("SolutionPriority", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
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
