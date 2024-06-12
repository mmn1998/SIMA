using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class PreRequisiteServicesConfiguration : IEntityTypeConfiguration<PreRequisiteServices>
{
    public void Configure(EntityTypeBuilder<PreRequisiteServices> entity)
    {
        entity.ToTable("PreRequisiteServices", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new PreRequisiteServicesId(v))
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
        entity.HasOne(d => d.Service).WithMany(p => p.PreRequisiteServicess)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.PreRequiredServiceId)
      .HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.PreRequiredService).WithMany(p => p.RequiredServicess)
                .HasForeignKey(d => d.PreRequiredServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

