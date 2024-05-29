using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.CriticalActivities;

public class CriticalActivityConfiguration : IEntityTypeConfiguration<CriticalActivity>
{
    public void Configure(EntityTypeBuilder<CriticalActivity> entity)
    {
        entity.ToTable("CriticalActivity", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new CriticalActivityId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();


        entity.Property(x => x.DepartmentId)
            .HasConversion(x => x.Value, x => new DepartmentId(x));

        entity.HasOne(x => x.Department)
            .WithMany(x => x.CriticalActivities)
            .HasForeignKey(x => x.DepartmentId);
        
        entity.Property(x => x.TechnicalResponsibleId)
            .HasConversion(x => x.Value, x => new StaffId(x));

        entity.HasOne(x => x.TechnicalResponsible)
            .WithMany(x => x.CriticalActivityTechnicalResponsibles)
            .HasForeignKey(x => x.TechnicalResponsibleId);
        
        entity.Property(x => x.TechnicalSupervisorId)
            .HasConversion(x => x.Value, x => new StaffId(x));

        entity.HasOne(x => x.TechnicalSupervisor)
            .WithMany(x => x.CriticalActivityTechnicalSupervisors)
            .HasForeignKey(x => x.TechnicalSupervisorId);
    }
}