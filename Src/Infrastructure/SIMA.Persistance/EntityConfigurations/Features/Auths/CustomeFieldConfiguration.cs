using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.CustomeFields.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFields.ValueObjects;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class CustomeFieldConfiguration : IEntityTypeConfiguration<CustomeField>
{
    public void Configure(EntityTypeBuilder<CustomeField> entity)
    {
        entity.ToTable("Configuration", "Basic");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new CustomeFieldId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.EnglishKey).IsUnicode(false);
        entity.Property(x => x.IsMandatory).HasMaxLength(1).IsFixedLength();

        entity.Property(x => x.CustomeFieldTypeId)
         .HasConversion(
          v => v.Value,
          v => new CustomeFieldTypeId(v));

        entity.HasOne(d => d.CustomeFieldType)
            .WithMany(d => d.CustomeFields)
            .HasForeignKey(d => d.CustomeFieldTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.MainAggregateId)
         .HasConversion(
          v => v.Value,
          v => new MainAggregateId(v));

        entity.HasOne(d => d.MainAggregate)
            .WithMany(d => d.CustomeFields)
            .HasForeignKey(d => d.MainAggregateId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
