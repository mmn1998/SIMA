using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierAddressBooks
{
    public class SupplierAddressBookConfiguration : IEntityTypeConfiguration<SupplierAddressBook>
    {
        public void Configure(EntityTypeBuilder<SupplierAddressBook> entity)
        {
            entity.ToTable("SupplierAddressBook", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierAddressBookId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();

            entity.Property(x => x.Address).HasColumnType("nvarchar(MAX)");
            entity.Property(x => x.PostalCode).HasMaxLength(10);

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierAddressBooks)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.AddressTypeId)
               .HasConversion(x => x.Value, x => new AddressTypeId(x));
            entity.HasOne(x => x.AddressType)
                .WithMany(x => x.SupplierAddressBooks)
                .HasForeignKey(x => x.AddressTypeId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
