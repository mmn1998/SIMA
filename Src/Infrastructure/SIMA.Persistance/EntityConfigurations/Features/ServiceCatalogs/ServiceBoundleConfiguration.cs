using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

internal class ServiceBoundleConfiguration : IEntityTypeConfiguration<ServiceBoundle>
{
    public void Configure(EntityTypeBuilder<ServiceBoundle> entity)
    {
        entity.ToTable("ServiceBoundle", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ServiceBoundleId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
              .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.ServiceCategoryId)
         .HasConversion(v => v.Value, v => new ServiceCategoryId(v));
        entity.HasOne(d => d.ServiceCategory).WithMany(p => p.ServiceBoundles)
                .HasForeignKey(d => d.ServiceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        //  entity.Property(x => x.ServiceId)
        //.HasConversion(v => v.Value, v => new ServiceId(v));
        //  entity.HasOne(d => d.Service).WithMany(p => p.ServiceCantracts)
        //          .HasForeignKey(d => d.ServiceId)
        //          .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

