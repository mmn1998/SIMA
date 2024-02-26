using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class AdminLocationAccessConfiguration : IEntityTypeConfiguration<AdminLocationAccess>
{
    public void Configure(EntityTypeBuilder<AdminLocationAccess> entity)
    {
        entity.ToTable("AdminLocationAccess", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AdminLocationAccessId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.LocationId).HasConversion(v => v.Value, v => new LocationId(v));
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Location).WithMany(p => p.AdminLocationAccesses)
            .HasForeignKey(d => d.LocationId)
            .HasConstraintName("FK_AdminLocationAccess_Location");

        entity.HasOne(d => d.User).WithMany(p => p.AdminLocationAccesses)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_AdminLocationAccess_User");
    }
}
