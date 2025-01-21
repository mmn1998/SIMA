using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;


public class DraftIssueTypeConfiguration : IEntityTypeConfiguration<DraftIssueType>
{
    public void Configure(EntityTypeBuilder<DraftIssueType> entity)
    {
        entity.ToTable("DraftIssueType", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new DraftIssueTypeId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
        entity.Property(x => x.Code).IsRequired().HasMaxLength(20);
        entity.HasIndex(x => x.Code).IsUnique();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}