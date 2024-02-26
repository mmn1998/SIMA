using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> entity)
    {
        entity.ToTable("Company", "Organization");
        entity.HasKey(e => e.Id).HasName("PK_Organization");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new CompanyId(v)).ValueGeneratedNever();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new CompanyId(v));

        //entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
        //    .HasForeignKey(d => d.ParentId)
        //    .HasConstraintName("FK_Company_Company");
    }
}
