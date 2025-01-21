using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class CurrencyOprationTypeConfiguration : IEntityTypeConfiguration<CurrencyOprationType>
{
    public void Configure(EntityTypeBuilder<CurrencyOprationType> entity)
    {
        entity.ToTable("CurrencyOprationType", "Bank");


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
    }
}