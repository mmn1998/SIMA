using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyServiceConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyService>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyService> entity)
    {
        entity.ToTable("BCP", "BusinessContinuityStrategyService");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityStrategyServiceId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
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
            .WithMany(x => x.BusinessContinuityStrategyServices)
            .HasForeignKey(x => x.BusinessContinuityStategyId);
    }
}
