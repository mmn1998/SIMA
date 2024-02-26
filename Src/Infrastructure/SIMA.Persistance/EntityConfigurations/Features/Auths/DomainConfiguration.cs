using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class DomainConfiguration : IEntityTypeConfiguration<Domain.Models.Features.Auths.Domains.Entities.Domain>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Features.Auths.Domains.Entities.Domain> entity)
    {
        entity.ToTable("Domain", "Authentication");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DomainId(v)).ValueGeneratedNever();
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
