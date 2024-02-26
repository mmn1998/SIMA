using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> entity)
    {
        entity.ToTable("Issue", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Description).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IssueTypeId)
       .HasConversion(
        v => v.Value,
        v => new IssueTypeId(v));
        entity.HasOne(d => d.IssueType).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssuePriorityId)
       .HasConversion(
        v => v.Value,
        v => new IssuePriorityId(v));
        entity.HasOne(d => d.IssuePriority).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssuePriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssueWeightCategoryId)
       .HasConversion(
        v => v.Value,
        v => new IssueWeightCategoryId(v));
        entity.HasOne(d => d.IssueWeightCategory).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueWeightCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
