using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths.SupplierRanks
{
    public class SupplierRankConfiguration : IEntityTypeConfiguration<SupplierRank>
    {
        public void Configure(EntityTypeBuilder<SupplierRank> entity)
        {
            entity.ToTable("SupplierRank", "Basic");
            entity.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new SupplierRankId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.Code)
                        .HasMaxLength(20).IsUnicode();
            entity.HasIndex(e => e.Code).IsUnique();
            
        }
    }
}
