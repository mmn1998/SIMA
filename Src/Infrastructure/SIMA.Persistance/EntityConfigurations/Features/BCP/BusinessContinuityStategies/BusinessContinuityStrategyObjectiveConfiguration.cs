using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyObjectiveConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyObjective>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyObjective> entity)
    {
        entity.ToTable("BCP", "BusinessContinuityStrategyObjective");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityStrategyObjectiveId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Code).HasMaxLength(50).IsUnicode(false);
        entity.Property(x => x.Title).HasMaxLength(200);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessContinuityStategyId)
            .HasConversion(
            x => x.Value,
            x => new BusinessContinuityStrategyId(x)
            );
        entity.HasOne(x => x.BusinessContinuityStategy)
            .WithMany(x => x.BusinessContinuityStrategyObjectives)
            .HasForeignKey(x => x.BusinessContinuityStategyId);
    }
}
