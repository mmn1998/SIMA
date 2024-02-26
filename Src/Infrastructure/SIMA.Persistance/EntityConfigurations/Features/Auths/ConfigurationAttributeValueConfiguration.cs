using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ConfigurationAttributeValueConfiguration : IEntityTypeConfiguration<ConfigurationAttributeValue>
{
    public void Configure(EntityTypeBuilder<ConfigurationAttributeValue> entity)
    {
        entity.ToTable("ConfigurationAttributeValue", "Basic");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationAttributeValueId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.ConfigurationAttributeId).HasConversion(v => v.Value, v => new ConfigurationAttributeId(v));
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.IsUserConfige)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Value).HasMaxLength(4000);

        entity.HasOne(d => d.ConfigurationAttribute).WithMany(p => p.ConfigurationAttributeValues)
            .HasForeignKey(d => d.ConfigurationAttributeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ConfigurationAttributeValue_A");
    }
}
