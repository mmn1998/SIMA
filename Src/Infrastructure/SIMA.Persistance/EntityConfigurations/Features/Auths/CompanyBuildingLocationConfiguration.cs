using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Entities;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class CompanyBuildingLocationConfiguration : IEntityTypeConfiguration<CompanyBuildingLocation>
{
    public void Configure(EntityTypeBuilder<CompanyBuildingLocation> entity)
    {
        entity.ToTable("CompanyBuildingLocation", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new CompanyBuildingLocationId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);

        entity.Property(x => x.CompanyId)
         .HasConversion(
          v => v.Value,
          v => new CompanyId(v));

        entity.HasOne(d => d.Company)
            .WithMany(d => d.CompanyBuildingLocations)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LocationId)
         .HasConversion(
          v => v.Value,
          v => new LocationId(v));

        entity.HasOne(d => d.Location)
            .WithMany(d => d.CompanyBuildingLocations)
            .HasForeignKey(d => d.LocationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
