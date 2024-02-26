using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueCommentConfiguration : IEntityTypeConfiguration<IssueComment>
{
    public void Configure(EntityTypeBuilder<IssueComment> entity)
    {
        entity.ToTable("IssueComment", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueCommentId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Comment).IsUnicode();
        entity.Property(x => x.IssueId)
               .HasConversion(
                v => v.Value,
                v => new IssueId(v));
        entity.HasOne(d => d.Issue).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
