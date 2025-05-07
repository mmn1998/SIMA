using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ResponsibleTypeConfiguration : IEntityTypeConfiguration<ResponsibleType>
{
    public void Configure(EntityTypeBuilder<ResponsibleType> entity)
    {
        entity.ToTable("ResponsibleType", "Basic");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ResponsibleTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(20);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
