using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.sanazConfiguration;

internal class ServiceUserConfiguration : IEntityTypeConfiguration<ServiceUser>
{
    public void Configure(EntityTypeBuilder<ServiceUser> entity)
    {
        entity.ToTable("ServiceUser", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceUserId(v))
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
        entity.HasOne(d => d.Service).WithMany(p => p.ServiceUsers)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.ServiceUserTypeId)
      .HasConversion(v => v.Value, v => new ServiceUserTypeId(v));
        entity.HasOne(d => d.ServiceUserType).WithMany(p => p.ServiceUsers)
                .HasForeignKey(d => d.ServiceUserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

