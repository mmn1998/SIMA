using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueHistoryConfiguration : IEntityTypeConfiguration<IssueHistory>
{
    public void Configure(EntityTypeBuilder<IssueHistory> entity)
    {
        entity.ToTable("IssueHistory", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(x => x.IssueId)
               .HasConversion(
                v => v.Value,
                v => new IssueId(v));
        entity.HasOne(d => d.Issue).WithMany(p => p.IssueHistories)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
