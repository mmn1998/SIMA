using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;
internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> entity)
    {
        entity.ToTable("Service", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new ServiceId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
                     .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.TechnicalResponsibleId)
       .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.StaffTechnicalResponsible).WithMany(p => p.ServiceTechnicalResponsibles)
                .HasForeignKey(d => d.TechnicalResponsibleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ServiceBoundleId)
       .HasConversion(v => v.Value, v => new ServiceBoundleId(v));
        entity.HasOne(d => d.ServiceBoundle).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceBoundleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BusinessResponsibleId)
       .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.StaffBusinessResponsible).WithMany(p => p.ServiceTBusinessResponsibles)
                .HasForeignKey(d => d.BusinessResponsibleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.TechnicalSupportId)
       .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.StaffTechnicalSupport).WithMany(p => p.ServiceTechnicalSupports)
                .HasForeignKey(d => d.TechnicalSupportId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.TechnicalSupervisorId)
       .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.StaffTechnicalSupervisor).WithMany(p => p.ServiceTechnicalSupervisors)
                .HasForeignKey(d => d.TechnicalSupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ServicePriorityId)
       .HasConversion(v => v.Value, v => new ServicePriorityId(v));
        entity.HasOne(d => d.ServicePriority).WithMany(p => p.Services)
            .HasForeignKey(d => d.ServicePriorityId);

        entity.Property(x => x.OwnerDepartmentId)
            .HasConversion(v => v.Value, v => new DepartmentId(v));
        entity.HasOne(d => d.OwnerDepartment).WithMany(p => p.OwnerService)
            .HasForeignKey(d => d.OwnerDepartmentId);
        entity.Property(x => x.TechnicalSupervisorDepartmentId)
            .HasConversion(v => v.Value, v => new DepartmentId(v));
        entity.HasOne(d => d.TechnicalSupervisorDepartment).WithMany(p => p.TechnicalSupervisorServices)
            .HasForeignKey(d => d.TechnicalSupervisorDepartmentId);
    }
}
