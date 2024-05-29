using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.CriticalActivities;

public class CriticalActivityServiceConfiguration : IEntityTypeConfiguration<CriticalActivityService>
{
    public void Configure(EntityTypeBuilder<CriticalActivityService> entity)
    {
        entity.ToTable("CriticalActivityService", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new CriticalActivityServiceId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();


        entity.Property(x => x.CriticalActivityId)
            .HasConversion(x => x.Value, x => new CriticalActivityId(x));

        entity.HasOne(x => x.CriticalActivity)
            .WithMany(x => x.CriticalActivityServices)
            .HasForeignKey(x => x.CriticalActivityId);
        
        entity.Property(x => x.ServiceId)
            .HasConversion(x => x.Value, x => new ServiceId(x));

        entity.HasOne(x => x.Service)
            .WithMany(x => x.CriticalActivityServices)
            .HasForeignKey(x => x.ServiceId);
    }
}
