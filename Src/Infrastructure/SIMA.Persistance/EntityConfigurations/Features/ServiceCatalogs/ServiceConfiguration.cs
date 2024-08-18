using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
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

        entity.Property(x => x.ServiceBoundleId)
             .HasConversion(v => v.Value, v => new ServiceBoundleId(v));
        entity.HasOne(d => d.ServiceBoundle).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceBoundleId).OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IsCriticalService).HasMaxLength(1).IsFixedLength();

        entity.Property(x => x.TechnicalSupervisorDepartmentId)
            .HasConversion(v => v.Value, v => new DepartmentId(v));
        entity.HasOne(d => d.TechnicalSupervisorDepartment).WithMany(p => p.TechnicalSupervisorServices)
            .HasForeignKey(d => d.TechnicalSupervisorDepartmentId);
        
        entity.Property(x => x.ServiceStatusId)
            .HasConversion(v => v.Value, v => new ServiceStatusId(v));
        entity.HasOne(d => d.ServiceStatus).WithMany(p => p.Services)
            .HasForeignKey(d => d.ServiceStatusId);
    }
}
