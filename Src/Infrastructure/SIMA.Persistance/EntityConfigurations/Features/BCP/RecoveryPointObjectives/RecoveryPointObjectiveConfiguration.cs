using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveConfiguration : IEntityTypeConfiguration<RecoveryPointObjective>
{
    public void Configure(EntityTypeBuilder<RecoveryPointObjective> entity)
    {
        entity.ToTable("RecoveryPointObjective", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new RecoveryPointObjectiveId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.RpoFrom).HasMaxLength(10);
        entity.Property(e => e.RpoTo).HasMaxLength(10);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}