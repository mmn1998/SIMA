using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Role", "Authentication");

            entity.HasIndex(e => new { e.Code, e.EnglishKey }).IsUnique();

            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RoleId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EnglishKey)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(50);
        }
    }
}
