using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssuePriorityConfiguration : IEntityTypeConfiguration<IssuePriority>
{
    public void Configure(EntityTypeBuilder<IssuePriority> entity)
    {
        entity.ToTable("IssuePriority", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssuePriorityId(v))
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
