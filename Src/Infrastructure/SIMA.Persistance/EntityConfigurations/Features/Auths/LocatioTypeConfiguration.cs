using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class LocatioTypeConfiguration : IEntityTypeConfiguration<LocationType>
{
    public void Configure(EntityTypeBuilder<LocationType> entity)
    {
        entity.ToTable("LocationType", "Basic");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new LocationTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedNever();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new LocationTypeId(v));

        //entity.HasOne(d => d.ActiveStatus).WithMany(p => p.LocationTypes)
        //    .HasForeignKey(d => d.ActiveStatusId)
        //    .HasConstraintName("FK_LocationType_ActiveStatus");

        //entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
        //    .HasForeignKey(d => d.ParentId)
        //    .HasConstraintName("FK_LocationType_LocationType");
    }
}
