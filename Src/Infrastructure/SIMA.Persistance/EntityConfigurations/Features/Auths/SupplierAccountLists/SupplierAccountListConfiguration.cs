using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierAccountLists
{
    public class SupplierAccountListConfiguration : IEntityTypeConfiguration<SupplierAccountList>
    {
        public void Configure(EntityTypeBuilder<SupplierAccountList> entity)
        {
            entity.ToTable("SupplierAccountList", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierAccountListId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();

            entity.Property(x => x.IBAN).HasMaxLength(26);

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierAccountLists)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
