using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> entity)
    {
        entity.ToTable("Staff", "Organization");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new StaffId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ManagerId).HasConversion(v => v.Value, v => new ProfileId(v));
        entity.Property(e => e.ProfileId).HasConversion(v => v.Value, v => new ProfileId(v));
        entity.Property(e => e.PositionId).HasConversion(v => v.Value, v => new PositionId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.StaffNumber)
            .HasMaxLength(20)
            .IsUnicode(false);

        entity.HasOne(d => d.Manager).WithMany(p => p.StaffManagers)
            .HasForeignKey(d => d.ManagerId)
            .HasConstraintName("FK_Staff_Profile_manager");

        entity.HasOne(d => d.Position).WithMany(p => p.Staff)
            .HasForeignKey(d => d.PositionId)
            .HasConstraintName("FK_Staff_Position");

        entity.HasOne(d => d.Profile).WithMany(p => p.StaffProfiles)
            .HasForeignKey(d => d.ProfileId)
            .HasConstraintName("FK_Staff_Profile");
    }
}