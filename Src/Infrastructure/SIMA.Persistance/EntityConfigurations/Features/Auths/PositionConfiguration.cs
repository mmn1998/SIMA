using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.PositionTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> entity)
    {
        entity.ToTable("Position", "Organization");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new PositionId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.DepartmentId).HasConversion(v => v.Value, v => new DepartmentId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);

        entity.HasOne(d => d.Department).WithMany(p => p.Positions)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("FK_Position_Department");
        
        entity.Property(x => x.BranchId)
         .HasConversion(
          v => v.Value,
          v => new BranchId(v));

        entity.HasOne(d => d.Branch)
            .WithMany(d => d.Positions)
            .HasForeignKey(d => d.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PositionLevelId)
         .HasConversion(
          v => v.Value,
          v => new PositionLevelId(v));

        entity.HasOne(d => d.PositionLevel)
            .WithMany(d => d.Positions)
            .HasForeignKey(d => d.PositionLevelId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PositionTypeId)
         .HasConversion(
          v => v.Value,
          v => new PositionTypeId(v));

        entity.HasOne(d => d.PositionType)
            .WithMany(d => d.Positions)
            .HasForeignKey(d => d.PositionTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
