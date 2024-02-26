using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class UserConfigConfiguration : IEntityTypeConfiguration<UserConfig>
{
    public void Configure(EntityTypeBuilder<UserConfig> entity)
    {
        entity.ToTable("UserConfig", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new UserConfigId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.ConfigurationId).HasConversion(v => v.Value, v => new ConfigurationAttributeId(v));
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.KeyValue)
            .HasMaxLength(4000)
            .IsUnicode(false);
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.UserId).HasConversion(v => v.Value, v => new UserId(v));

        entity.HasOne(d => d.Configuration).WithMany(p => p.UserConfigs)
            .HasForeignKey(d => d.ConfigurationId)
            .HasConstraintName("FK_UserConfig_ConfigurationAttribute");

        entity.HasOne(d => d.User).WithMany(p => p.UserConfigs)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserConfig_User");
    }
}
