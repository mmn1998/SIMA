using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("Users", "Authentication");

            entity.HasIndex(e => e.Username, "index_Username").IsUnique();

            entity.HasIndex(e => new { e.Username, e.ProfileId, e.CompanyId }, "index_Username_ID").IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new UserId(v)).ValueGeneratedNever();
            entity.Property(e => e.CompanyId).HasConversion(v => v.Value, v => new CompanyId(v));
            entity.Property(e => e.ProfileId).HasConversion(v => v.Value, v => new ProfileId(v));
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsLoggedIn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.OwnsOne(p => p.Password, p =>
            {
                p.Property(pp => pp.SecretKey).HasColumnName("SecretKey").HasColumnType("nvarchar(max)");
                p.Property(pp => pp.Password).HasColumnName("Password").HasMaxLength(128).IsUnicode(false);
            });
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.SecretKey).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.IsFirstLogin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.IsLocked)
               .HasMaxLength(1)
               .IsUnicode(false)
               .IsFixedLength();

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_User_Company");

            entity.HasOne(d => d.Profile).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("FK_User_Profile");
        }
    }
}
