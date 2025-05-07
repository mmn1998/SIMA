using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans
{
    public class BusinessContinuityPlanStratgyConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanStratgy>
    {
        public void Configure(EntityTypeBuilder<BusinessContinuityPlanStratgy> entity)
        {
            entity.ToTable("BusinessContinuityPlanStratgy", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new BusinessContinuityPlanStratgyId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.BusinessContinuityPlanId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.BusinessContinuityPlan)
                .WithMany(x => x.BusinessContinuityPlanStratgies)
                .HasForeignKey(x => x.BusinessContinuityPlanId);

            entity.Property(x => x.BusinessContinuityStratgyId)
            .HasConversion(
               x => x.Value,
               x => new BusinessContinuityStrategyId(x)
               );
            entity.HasOne(x => x.BusinessContinuityStrategy)
                .WithMany(x => x.BusinessContinuityPlanStratgies)
                .HasForeignKey(x => x.BusinessContinuityStratgyId);
        }
    }
}
