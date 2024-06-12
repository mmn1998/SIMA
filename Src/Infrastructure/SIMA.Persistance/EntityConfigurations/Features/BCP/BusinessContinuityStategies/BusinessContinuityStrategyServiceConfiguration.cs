using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyServiceConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyService>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyService> entity)
    {
        entity.ToTable("BusinessContinuityStrategyService", "BCP");
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
        
        entity.Property(x => x.ServiceId)
            .HasConversion(
            x => x.Value,
            x => new ServiceId(x)
            );
        entity.HasOne(x => x.Service)
            .WithMany(x => x.BusinessContinuityStrategyServices)
            .HasForeignKey(x => x.ServiceId);
    }
}
