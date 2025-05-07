using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class AccessTypeConfiguration : IEntityTypeConfiguration<AccessType>
{
    public void Configure(EntityTypeBuilder<AccessType> entity)
    {
        entity.ToTable("AccessType", "Basic");
        entity.Property(e => e.Id)
            .HasConversion(v => v.Value,
            v => new AccessTypeId(v))
            .ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
