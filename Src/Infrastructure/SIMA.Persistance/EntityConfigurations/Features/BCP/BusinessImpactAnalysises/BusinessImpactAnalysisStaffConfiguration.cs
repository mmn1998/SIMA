using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisStaffConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisStaff>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisStaff> entity)
    {
        entity.ToTable("BusinessImpactAnalysisStaff", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisStaffId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(
            x => x.Value,
            x => new BusinessImpactAnalysisId(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisStaff)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
        
        entity.Property(x => x.StaffId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessImpactAnalysisStaff)
            .HasForeignKey(x => x.StaffId);
        
        entity.Property(x => x.DepartmentId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Department)
            .WithMany(x => x.BusinessImpactAnalysisStaff)
            .HasForeignKey(x => x.DepartmentId);
        
        entity.Property(x => x.BranchId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Branch)
            .WithMany(x => x.BusinessImpactAnalysisStaff)
            .HasForeignKey(x => x.BranchId);
    }
}