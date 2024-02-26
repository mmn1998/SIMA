using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class SysConfiguration : IEntityTypeConfiguration<SysConfig>
{
    public void Configure(EntityTypeBuilder<SysConfig> entity)
    {
        entity.ToTable("SysConfig", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new SysConfigId(v)).ValueGeneratedNever();
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

        entity.HasOne(d => d.Configuration).WithMany(p => p.SysConfigs)
            .HasForeignKey(d => d.ConfigurationId)
            .HasConstraintName("FK_SysConfig_ConfigurationAttribute");
    }
}
