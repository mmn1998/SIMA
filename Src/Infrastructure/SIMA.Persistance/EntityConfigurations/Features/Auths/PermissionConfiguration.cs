using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_Permission_1");

            entity.ToTable("Permission", "Authentication");

            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new PermissionId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DomainId).HasConversion(v => v.Value, v => new DomainId(v));
            entity.Property(e => e.EnglishKey)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Domain).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.DomainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_Domain");
        }
    }
}
