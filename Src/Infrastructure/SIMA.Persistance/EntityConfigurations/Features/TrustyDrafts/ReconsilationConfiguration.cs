using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Reconsilations.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class ReconsilationConfiguration : IEntityTypeConfiguration<Reconsilation>
{
    public void Configure(EntityTypeBuilder<Reconsilation> entity)
    {
        entity.ToTable("Reconsilation", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ReconsilationId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        //entity.Property(x => x.LetterNumber).HasMaxLength(20);
        //entity.Property(x => x.PartNumber).HasMaxLength(20);
        //entity.Property(x => x.SwiftMessage).HasMaxLength(3);
        entity.Property(x => x.Description).HasMaxLength(4000);
        entity.Property(x => x.IsInformedByBranch).HasMaxLength(1).IsFixedLength();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        //entity.Property(e => e.BrokerId)
        //    .HasConversion
        //        (v => v.Value,
        //        v => new(v));
        //entity.HasOne(x => x.Broker)
        //    .WithMany(x => x.Reconsilations)
        //    .HasForeignKey(x => x.BrokerId)
        //    .OnDelete(DeleteBehavior.ClientSetNull);


        //entity.Property(e => e.BranchId)
        //    .HasConversion
        //        (v => v.Value,
        //        v => new(v));
        //entity.HasOne(x => x.Branch)
        //    .WithMany(x => x.Reconsilations)
        //    .HasForeignKey(x => x.BranchId)
        //    .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.ReconsilationTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.ReconsilationType)
            .WithMany(x => x.Reconsilations)
            .HasForeignKey(x => x.ReconsilationTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.TrustyDraftId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.TrustyDraft)
            .WithMany(x => x.Reconsilations)
            .HasForeignKey(x => x.TrustyDraftId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
