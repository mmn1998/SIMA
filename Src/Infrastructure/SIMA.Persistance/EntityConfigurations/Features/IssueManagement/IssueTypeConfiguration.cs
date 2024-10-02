using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

public class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
{
    public void Configure(EntityTypeBuilder<IssueType> entity)
    {
        entity.ToTable("IssueType", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueTypeId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
    }
}
