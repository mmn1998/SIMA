using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> entity)
    {
        entity.ToTable("Location", "Basic");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new LocationId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.LocationTypeId).HasConversion(v => v.Value, v => new LocationTypeId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new LocationId(v));

        //entity.HasOne(d => d.ActiveStatus).WithMany(p => p.Locations)
        //    .HasForeignKey(d => d.ActiveStatusId)
        //    .HasConstraintName("FK_Location_ActiveStatus");

        entity.HasOne(d => d.LocationType).WithMany(p => p.Locations)
            .HasForeignKey(d => d.LocationTypeId)
            .HasConstraintName("FK_Location_LocationType");

        //entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
        //    .HasForeignKey(d => d.ParentId)
        //    .HasConstraintName("FK_Location_Location_parent");


    }
}
