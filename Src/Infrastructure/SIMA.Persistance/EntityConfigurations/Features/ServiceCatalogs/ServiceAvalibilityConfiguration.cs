using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ServiceAvalibilityConfiguration : IEntityTypeConfiguration<ServiceAvalibility>
{
    public void Configure(EntityTypeBuilder<ServiceAvalibility> entity)
    {
        entity.ToTable("ServiceAvalibility", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceAvalibilityId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(e => e.ServiceAvalibilityStartTime)
       .HasConversion(
        v => new TimeSpan(v.Hour, v.Minute, v.Second),  
        v => TimeOnly.FromTimeSpan(v))                  
      .HasColumnType("TIME(7)");


        entity.Property(e => e.ServiceAvalibilityEndTime)
      .HasConversion(
       v => new TimeSpan(v.Hour, v.Minute, v.Second),
       v => TimeOnly.FromTimeSpan(v))
     .HasColumnType("TIME(7)");


        entity.Property(x => x.ServiceId)
         .HasConversion(v => v.Value, v => new ServiceId(v));
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceAvalibilities)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

