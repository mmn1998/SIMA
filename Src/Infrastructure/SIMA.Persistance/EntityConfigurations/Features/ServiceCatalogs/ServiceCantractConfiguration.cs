using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ServiceCantractConfiguration : IEntityTypeConfiguration<ServiceCantract>
{
    public void Configure(EntityTypeBuilder<ServiceCantract> entity)
    {
        entity.ToTable("ServiceCantract", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceCantractId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ServiceId)
         .HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceCantracts)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

