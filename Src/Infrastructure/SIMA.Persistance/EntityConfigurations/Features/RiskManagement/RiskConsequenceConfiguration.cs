using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskConsequenceConfiguration : IEntityTypeConfiguration<RiskConsequence>
{
    public void Configure(EntityTypeBuilder<RiskConsequence> entity)
    {
        entity.ToTable("RiskConsequence", "RiskManagement");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConsequenceCategoryId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ConsequenceCategory)
            .WithMany(x => x.RiskConsequences)
            .HasForeignKey(x => x.ConsequenceCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConsequenceLevelId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.ConsequenceLevel)
            .WithMany(x => x.RiskConsequences)
            .HasForeignKey(x => x.ConsequenceLevelId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}