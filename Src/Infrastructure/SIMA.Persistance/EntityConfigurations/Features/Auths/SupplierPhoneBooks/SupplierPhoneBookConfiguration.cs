using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierPhoneBooks
{
    public class SupplierPhoneBookConfiguration : IEntityTypeConfiguration<SupplierPhoneBook>
    {
        public void Configure(EntityTypeBuilder<SupplierPhoneBook> entity)
        {
            entity.ToTable("SupplierPhoneBook", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierPhoneBookId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();

            entity.Property(x => x.PhoneNumber).HasMaxLength(20);

            entity.Property(x => x.SupplierId)
                .HasConversion(x => x.Value, x => new SupplierId(x));
            entity.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierPhoneBooks)
                .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.PhoneTypeId)
                .HasConversion(x => x.Value, x => new PhoneTypeId(x));
            entity.HasOne(x => x.PhoneType)
                .WithMany(x => x.SupplierPhoneBooks)
                .HasForeignKey(x => x.PhoneTypeId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
