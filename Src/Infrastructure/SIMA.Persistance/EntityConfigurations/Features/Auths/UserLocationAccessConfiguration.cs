using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class UserLocationAccessConfiguration : IEntityTypeConfiguration<UserLocationAccess>
{
    public void Configure(EntityTypeBuilder<UserLocationAccess> entity)
    {
        entity.ToTable("UserLocationAccess", "Authentication");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserLocationAccessId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.LocationId).HasConversion(v => v.Value, v => new LocationId(v));
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));

        entity.HasOne(d => d.Location).WithMany(p => p.UserLocationAccesses)
            .HasForeignKey(d => d.LocationId)
            .HasConstraintName("FK_UserLocationAccess_Location");

        entity.HasOne(d => d.User).WithMany(p => p.UserLocationAccesses)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserLocationAccess_User");
    }
}
