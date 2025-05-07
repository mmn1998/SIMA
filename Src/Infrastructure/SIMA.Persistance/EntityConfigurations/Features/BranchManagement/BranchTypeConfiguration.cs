using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BranchManagement;

public class BranchTypeConfiguration : IEntityTypeConfiguration<BranchType>
{
    public void Configure(EntityTypeBuilder<BranchType> entity)
    {
        entity.ToTable("BranchType", "Bank");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Id)
           .HasColumnName("Id")
           .HasConversion(
               v => v.Value,
               v => new BranchTypeId(v))
           .ValueGeneratedNever();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        entity.Property(e => e.ModifyAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
    }
}
