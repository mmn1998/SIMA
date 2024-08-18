using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategy>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategy> entity)
    {
        entity.ToTable("BusinessContinuityStrategy", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityStrategyId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.IsStableStrategy).HasMaxLength(1);
        entity.Property(e => e.Title).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(x => x.StrategyTypeId)
           .HasConversion(
           x => x.Value,
           x => new StrategyTypeId(x)
           );
        entity.HasOne(x => x.StrategyType)
            .WithMany(x => x.BusinessContinuityStrategys)
            .HasForeignKey(x => x.StrategyTypeId);

    }
}