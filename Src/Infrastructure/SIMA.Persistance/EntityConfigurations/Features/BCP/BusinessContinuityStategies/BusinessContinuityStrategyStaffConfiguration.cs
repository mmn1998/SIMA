using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyStaffConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyStaff>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyStaff> entity)
    {
        entity.ToTable("BCP", "BusinessContinuityStrategyService");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityStrategyStaffId(v)).ValueGeneratedNever();
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
            .WithMany(x => x.BusinessContinuityStrategyStaff)
            .HasForeignKey(x => x.BusinessContinuityStategyId);

        entity.Property(x => x.StaffId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessContinuityStrategyStaff)
            .HasForeignKey(x => x.StaffId);
    }
}