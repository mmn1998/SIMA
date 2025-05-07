using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ProductResponsibleConfiguration : IEntityTypeConfiguration<ProductResponsible>
{
    public void Configure(EntityTypeBuilder<ProductResponsible> entity)
    {
        entity.ToTable("ProductResponsible", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(v => v.Value, v => new ProductResponsibleId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ProductId)
            .HasConversion(v => v.Value, v => new ProductId(v));
        entity.HasOne(it => it.Product).WithMany(it => it.ProductResponsibles)
            .HasForeignKey(it => it.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ResponsilbeId)
            .HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(it => it.Responsilbe).WithMany(it => it.ProductResponsibles)
            .HasForeignKey(it => it.ResponsilbeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ResponsibleTypeId)
            .HasConversion(v => v.Value, v => new ResponsibleTypeId(v));

        entity.HasOne(it => it.ResponsibleType).WithMany(it => it.ProductResponsibles)
            .HasForeignKey(it => it.ResponsibleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.DepartmentId)
.HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Department)
            .WithMany(x => x.ProductResponsibles)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);


        entity.Property(x => x.BranchId)
        .HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Branch)
            .WithMany(x => x.ProductResponsibles)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

