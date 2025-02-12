using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies
{
    public class BusinessContinuityStratgyResponsibleConfiguration : IEntityTypeConfiguration<BusinessContinuityStratgyResponsible>
    {
        public void Configure(EntityTypeBuilder<BusinessContinuityStratgyResponsible> entity)
        {
            entity.ToTable("BusinessContinuityStratgyResponsible", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new BusinessContinuityStratgyResponsibleId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(x => x.IsForBackup).HasMaxLength(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.BusinessContinuityStrategyId)
                .HasConversion(
                x => x.Value,
                x => new BusinessContinuityStrategyId(x)
                );

            entity.HasOne(x => x.BusinessContinuityStrategy)
                .WithMany(x => x.BusinessContinuityStratgyResponsibles)
                .HasForeignKey(x => x.BusinessContinuityStrategyId);


            entity.Property(x => x.StaffId)
                .HasConversion(
                x => x.Value,
                x => new StaffId(x)
                );

            entity.HasOne(x => x.Staff)
                .WithMany(x => x.BusinessContinuityStratgyResponsibles)
                .HasForeignKey(x => x.StaffId);

            entity.Property(x => x.PlanResponsibilityId)
                .HasConversion(
                x => x.Value,
                x => new PlanResponsibilityId(x)
                );

            entity.HasOne(x => x.PlanResponsibility)
               .WithMany(x => x.BusinessContinuityStratgyResponsibles)
               .HasForeignKey(x => x.PlanResponsibilityId);

            entity.Property(x => x.DepartmentId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.Department)
               .WithMany(x => x.BusinessContinuityStratgyResponsibles)
               .HasForeignKey(x => x.DepartmentId);

            entity.Property(x => x.BranchId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
            entity.HasOne(x => x.Branch)
               .WithMany(x => x.BusinessContinuityStratgyResponsibles)
               .HasForeignKey(x => x.BranchId);
        }

    }
}
