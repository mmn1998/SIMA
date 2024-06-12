using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.ServicePriorities;

public class ServicePriorityConfiguration : IEntityTypeConfiguration<OrganizationalServicePriority>
{
    public void Configure(EntityTypeBuilder<OrganizationalServicePriority> entity)
    {
        entity.ToTable("ServicePriority", "BCP");
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new OrganizationalServicePriorityId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
    }
}
