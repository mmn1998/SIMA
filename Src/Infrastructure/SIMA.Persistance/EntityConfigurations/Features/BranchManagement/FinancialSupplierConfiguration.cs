using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class FinancialSupplierConfiguration : IEntityTypeConfiguration<FinancialSupplier>
{
    public void Configure(EntityTypeBuilder<FinancialSupplier> entity)
    {
        entity.ToTable("FinancialSupplier", "Bank");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Name).HasMaxLength(200);
        entity.Property(x => x.Code).HasMaxLength(20);
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.CustomerId)
            .HasConversion(v => v.Value,
            v => new(v));
        entity.HasOne(x => x.Customer)
            .WithMany(x => x.FinancialSuppliers)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}