using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        //entity.Property(x => x.ResponsilbeTypeId)
        //.HasConversion(x => x.Value, x => new ResponsilbeTypeId(x));

        //entity.HasOne(x => x.ResponsilbeType)
        //    .WithMany(x => x.CriticalActivityAssignStaffs)
        //    .HasForeignKey(x => x.ResponsilbeTypeId); //TODO
    }
}
