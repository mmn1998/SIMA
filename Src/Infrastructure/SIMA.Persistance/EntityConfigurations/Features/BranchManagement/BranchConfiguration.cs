using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BranchConfiguration : IEntityTypeConfiguration<Domain.Models.Features.BranchManagement.Branches.Entities.Branch>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Features.BranchManagement.Branches.Entities.Branch> entity)
    {
        entity.ToTable("Branch", "Bank");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BranchId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Address).HasMaxLength(500).IsUnicode();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(x => x.BranchTypeId)
            .HasConversion(
            v => v.Value,
            v => new BranchTypeId(v));
        entity.Property(x => x.DepartmentId)
            .HasConversion(
            v => v.Value,
            v => new DepartmentId(v));

        entity.Property(e => e.IsMultiCurrencyBranch)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        entity.Property(e => e.PostalCode).HasMaxLength(10);
        entity.HasOne(d => d.BranchType).WithMany(p => p.Branches)
            .HasForeignKey(d => d.BranchTypeId)
            .HasConstraintName("FK_Branch_BranchType");
        entity.HasOne(d => d.Department).WithMany(p => p.Branches)
            .HasForeignKey(d => d.DepartmentId);
    }
}
