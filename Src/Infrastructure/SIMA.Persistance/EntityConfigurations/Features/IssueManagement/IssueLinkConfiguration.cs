using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueLinkConfiguration : IEntityTypeConfiguration<IssueLink>
{
    public void Configure(EntityTypeBuilder<IssueLink> entity)
    {
        entity.ToTable("IssueLink", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueLinkId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.IssueId)
       .HasConversion(
        v => v.Value,
        v => new IssueId(v));
        entity.HasOne(d => d.Issue).WithMany(p => p.IssueLinks)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssueIdLinkedTo)
       .HasConversion(
        v => v.Value,
        v => new IssueId(v));
        entity.HasOne(d => d.IssueLinkedTo).WithMany(p => p.IssuesLinkedTo)
                .HasForeignKey(d => d.IssueIdLinkedTo)
                .OnDelete(DeleteBehavior.ClientSetNull);
        entity.Property(x => x.IssueIdLinkReasonTo)
       .HasConversion(
        v => v.Value,
        v => new IssueLinkReasonId(v));
        entity.HasOne(d => d.IssueLinkedReason).WithMany(p => p.IssueLinks)
                .HasForeignKey(d => d.IssueIdLinkReasonTo)
                .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
