using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.ValueObject;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class AssetAssignedStaffConfiguration : IEntityTypeConfiguration<AssetAssignedStaff>
{
    public void Configure(EntityTypeBuilder<AssetAssignedStaff> entity)
    {
        entity.ToTable("AssetAssignedStaffs", "Asset");
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new AssetAssignedStaffId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        
        entity.HasOne(d => d.Asset)
            .WithMany(d => d.AssetAssignedStaffs)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Staff)
            .WithMany(d => d.AssetAssignedStaffs)
            .HasForeignKey(d => d.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Department)
            .WithMany(d => d.AssetAssignedStaffs)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Branch)
            .WithMany(d => d.AssetAssignedStaffs)
            .HasForeignKey(d => d.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.ResponsibleType)
            .WithMany(d => d.AssetAssignedStaffs)
            .HasForeignKey(d => d.ResponsibleTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
    }
}