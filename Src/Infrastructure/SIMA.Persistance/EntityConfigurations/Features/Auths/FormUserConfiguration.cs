using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.Domains.Entities;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class FormUserConfiguration : IEntityTypeConfiguration<FormUser>
{
    public void Configure(EntityTypeBuilder<FormUser> entity)
    {
        entity.ToTable("FormUser", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new FormUserId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
          .IsRowVersion()
          .IsConcurrencyToken();
        entity.Property(x => x.FormId)
          .HasConversion(
           v => v.Value,
           v => new FormId(v));

        entity.Property(x => x.UserId)
          .HasConversion(
           v => v.Value,
           v => new UserId(v));

        entity.HasOne(d => d.User).WithMany(d => d.FormUsers).HasForeignKey(d => d.UserId);
        entity.HasOne(d => d.Form).WithMany(d => d.FormUsers).HasForeignKey(d => d.FormId);

    }
}
