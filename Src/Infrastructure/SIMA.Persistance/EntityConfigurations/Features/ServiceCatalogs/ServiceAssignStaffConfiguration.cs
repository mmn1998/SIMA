using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ServiceAssignStaffConfiguration : IEntityTypeConfiguration<ServiceAssignStaff>
{
    public void Configure(EntityTypeBuilder<ServiceAssignStaff> entity)
    {
        entity.ToTable("ServiceAssignStaff", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceAssignStaffId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.ServiceId)
      .HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceAssignStaffes)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.StaffId)
      .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.Staff).WithMany(p => p.ServiceAssignStaffes)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

