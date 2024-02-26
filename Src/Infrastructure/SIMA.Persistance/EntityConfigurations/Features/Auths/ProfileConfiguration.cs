using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> entity)
    {
        entity.ToTable("Profile", "Authentication");

        entity.HasIndex(e => e.NationalId, "index_national").IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ProfileId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.FatherName).HasMaxLength(50);
        entity.Property(e => e.FirstName).HasMaxLength(50);
        entity.Property(e => e.GenderId).HasConversion(v => v.Value, v => new GenderId(v));
        entity.Property(e => e.LastName).HasMaxLength(50);
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.NationalId)
            .HasMaxLength(12)
            .IsUnicode(false)
            .HasColumnName("NationalID");

        entity.HasOne(d => d.Gender).WithMany(p => p.Profiles)
            .HasForeignKey(d => d.GenderId)
            .HasConstraintName("FK_Profile_Gender");
    }
}
