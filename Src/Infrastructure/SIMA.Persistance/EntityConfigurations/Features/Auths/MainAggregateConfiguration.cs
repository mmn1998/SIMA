using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class MainAggregateConfiguration : IEntityTypeConfiguration<MainAggregate>
{
    public void Configure(EntityTypeBuilder<MainAggregate> entity)
    {
        entity.ToTable("MainAggregate", "Authentication");

        entity.HasIndex(e => e.Code, "index_Code").IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new MainAggregateId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.DomainId).HasConversion(v => v.Value, v => new DomainId(v));
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);

        entity.HasOne(d => d.Domain).WithMany(p => p.MainEntities)
            .HasForeignKey(d => d.DomainId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
