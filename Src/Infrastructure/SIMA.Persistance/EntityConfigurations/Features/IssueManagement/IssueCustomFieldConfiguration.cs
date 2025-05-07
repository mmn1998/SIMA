using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement;

internal class IssueCustomFieldConfiguration : IEntityTypeConfiguration<IssueCustomFeild>
{
    public void Configure(EntityTypeBuilder<IssueCustomFeild> entity)
    {
        entity.ToTable("IssueCustomFeild", "IssueManagement");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new IssueCustomFeildId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.IssueId)
    .HasConversion(
        v => v.Value,
        v => new IssueId(v));
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.IssueCustomFeilds)
            .HasForeignKey(x => x.IssueId);
    }
}
