using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class AddressBookConfiguration : IEntityTypeConfiguration<AddressBook>
{
    public void Configure(EntityTypeBuilder<AddressBook> entity)
    {
        entity.ToTable("AddressBook", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new AddressBookId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Address).HasMaxLength(2000);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.LocationId).HasConversion(v => v.Value, v => new LocationId(v));
        entity.Property(e => e.ProfileId).HasConversion(v => v.Value, v => new ProfileId(v));
        entity.Property(e => e.AddressTypeId).HasConversion(v => v.Value, v => new AddressTypeId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.PostalCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

        entity.HasOne(d => d.AddressType).WithMany(p => p.AddressBooks)
            .HasForeignKey(d => d.AddressTypeId)
            .HasConstraintName("FK_AddressBook_AddressType");

        entity.HasOne(d => d.Location).WithMany(p => p.AddressBooks)
            .HasForeignKey(d => d.LocationId)
            .HasConstraintName("FK_AddressBook_Location");

        entity.HasOne(d => d.Profile).WithMany(p => p.AddressBooks)
            .HasForeignKey(d => d.ProfileId)
            .HasConstraintName("FK_AddressBook_Profile");
    }
}
