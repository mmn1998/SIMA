using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ApiMethodActionConfiguration : IEntityTypeConfiguration<ApiMethodAction>
{
    public void Configure(EntityTypeBuilder<ApiMethodAction> entity)
    {
        entity.ToTable("ApiMethodAction", "Basic");
        entity.Property(e => e.Id)
            .HasConversion(v => v.Value,
            v => new ApiMethodActionId(v))
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
