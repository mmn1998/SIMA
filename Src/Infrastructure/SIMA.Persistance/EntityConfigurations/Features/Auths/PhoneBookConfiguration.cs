using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class PhoneBookConfiguration : IEntityTypeConfiguration<PhoneBook>
{
    public void Configure(EntityTypeBuilder<PhoneBook> entity)
    {
        entity.ToTable("PhoneBook", "Authentication");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new PhoneBookId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        entity.Property(e => e.PhoneTypeId).HasConversion(v => v.Value, v => new PhoneTypeId(v));
        entity.Property(e => e.ProfileId).HasConversion(v => v.Value, v => new ProfileId(v));

        entity.HasOne(d => d.PhoneType).WithMany(p => p.PhoneBooks)
            .HasForeignKey(d => d.PhoneTypeId)
            .HasConstraintName("FK_PhoneBook_PhonType");

        entity.HasOne(d => d.Profile).WithMany(p => p.PhoneBooks)
            .HasForeignKey(d => d.ProfileId)
            .HasConstraintName("FK_PhoneBook_Profile");
    }
}
