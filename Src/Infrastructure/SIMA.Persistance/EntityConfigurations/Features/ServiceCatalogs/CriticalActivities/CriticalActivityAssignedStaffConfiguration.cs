using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.CriticalActivities;

public class CriticalActivityAssignedStaffConfiguration : IEntityTypeConfiguration<CriticalActivityAssignedStaff>
{
    public void Configure(EntityTypeBuilder<CriticalActivityAssignedStaff> entity)
    {
        entity.ToTable("CriticalActivityAssignStaff", "ServiceCatalog");
        
        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new CriticalActivityAssignedStaffId(v))
            .ValueGeneratedNever();
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.CriticalActivityId)
            .HasConversion(x => x.Value, x => new CriticalActivityId(x));

        entity.HasOne(x => x.CriticalActivity)
            .WithMany(x => x.CriticalActivityAssignStaffs)
            .HasForeignKey(x => x.CriticalActivityId);
        
        entity.Property(x => x.StaffId)
            .HasConversion(x => x.Value, x => new StaffId(x));

        entity.HasOne(x => x.Staff)
            .WithMany(x => x.CriticalActivityAssignStaffs)
            .HasForeignKey(x => x.StaffId);

        entity.Property(x => x.ResponsilbeTypeId)
        .HasConversion(x => x.Value, x => new ResponsibleTypeId(x));

        entity.HasOne(x => x.ResponsilbeType)
            .WithMany(x => x.CriticalActivityAssignedStaffs)
            .HasForeignKey(x => x.ResponsilbeTypeId);

        entity.Property(x => x.DepartmentId)
        .HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Department)
            .WithMany(x => x.CriticalActivityAssignedStaffs)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BranchId)
        .HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Branch)
            .WithMany(x => x.CriticalActivityAssignedStaffs)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class CriticalActivityConfigurationItemConfiguration : IEntityTypeConfiguration<CriticalActivityConfigurationItem>
{
    public void Configure(EntityTypeBuilder<CriticalActivityConfigurationItem> entity)
    {
        entity.ToTable("CriticalActivityConfigurationItem", "ServiceCatalog");
        
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new CriticalActivityConfigurationItemId(v))
            .ValueGeneratedNever();
        
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.CriticalActivityId)
            .HasConversion(x => x.Value, x => new CriticalActivityId(x));

        entity.HasOne(x => x.CriticalActivity)
            .WithMany(x => x.CriticalActivityConfigurationItems)
            .HasForeignKey(x => x.CriticalActivityId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.ConfigurationItemId)
            .HasConversion(x => x.Value, x => new ConfigurationItemId(x));

        entity.HasOne(x => x.ConfigurationItem)
            .WithMany(x => x.CriticalActivityConfigurationItems)
            .HasForeignKey(x => x.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
