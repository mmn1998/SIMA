using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> entity)
    {
        entity.ToTable("UserType", "Basic");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserTypeId(v)).ValueGeneratedNever();

        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
    }
}