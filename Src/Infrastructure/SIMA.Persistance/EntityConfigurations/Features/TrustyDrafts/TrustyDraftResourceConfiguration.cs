using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class TrustyDraftResourceConfiguration : IEntityTypeConfiguration<TrustyDraftResource>
{
    public void Configure(EntityTypeBuilder<TrustyDraftResource> entity)
    {
        entity.ToTable("TrustyDraftResource", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(e => e.ResourceId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Resource)
            .WithMany(x => x.TrustyDraftResources)
            .HasForeignKey(x => x.ResourceId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.TrustyDraftResources)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}