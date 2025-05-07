using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ConfigurationTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ConfigurationAttributeConfiguration : IEntityTypeConfiguration<ConfigurationAttribute>
{
    public void Configure(EntityTypeBuilder<ConfigurationAttribute> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_onfigurationAttribute");

        entity.ToTable("ConfigurationAttribute", "Basic");

        entity.HasIndex(e => e.EnglishKey, "index_Key").IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationAttributeId(v)).ValueGeneratedNever();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.EnglishKey)
            .HasMaxLength(200)
            .IsUnicode(false);
        entity.Property(e => e.IsUserConfige)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(x => x.ConfigurationTypeId)
         .HasConversion(
          v => v.Value,
          v => new ConfigurationTypeId(v));

        entity.HasOne(d => d.ConfigurationType)
            .WithMany(d => d.ConfigurationAttributes)
            .HasForeignKey(d => d.ConfigurationTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
