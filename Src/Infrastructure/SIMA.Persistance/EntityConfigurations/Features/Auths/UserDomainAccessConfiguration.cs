using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class UserDomainAccessConfiguration : IEntityTypeConfiguration<UserDomainAccess>
{
    public void Configure(EntityTypeBuilder<UserDomainAccess> entity)
    {
        entity.ToTable("UserDomainAccess", "Authentication");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserDomainAccessId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.DomainId).HasConversion(v => v.Value, v => new DomainId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));

        entity.HasOne(d => d.Domain).WithMany(p => p.UserDomainAccesses)
            .HasForeignKey(d => d.DomainId)
            .HasConstraintName("FK_UserDomainAccess_Domain");

        entity.HasOne(d => d.User).WithMany(p => p.UserDomainAccesses)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserDomainAccess_User");
    }
}
