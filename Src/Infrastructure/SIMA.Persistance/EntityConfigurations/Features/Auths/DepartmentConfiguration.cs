using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> entity)
    {
        entity.ToTable("Department", "Organization");

        entity.HasIndex(e => e.Id, "IX_Department");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DepartmentId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CompanyId).HasConversion(v => v.Value, v => new CompanyId(v));
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.LocationId).HasConversion(v => v.Value, v => new LocationId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new DepartmentId(v));

        entity.HasOne(d => d.Company).WithMany(p => p.Departments)
            .HasForeignKey(d => d.CompanyId)
            .HasConstraintName("FK_Department_Company");

        entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
            .HasForeignKey(d => d.ParentId)
            .HasConstraintName("FK_Department_Department_parent");
    }
}
