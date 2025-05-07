using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.AnalysisValues;

public class ConsequenceIntensionDescriptionConfiguration : IEntityTypeConfiguration<ConsequenceIntensionDescription>
{
    public void Configure(EntityTypeBuilder<ConsequenceIntensionDescription> entity)
    {
        entity.ToTable("ConsequenceIntensionDescription", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConsequenceId)
              .HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Consequence)
            .WithMany(x => x.ConsequenceIntensionDescriptions)
            .HasForeignKey(x => x.ConsequenceId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConsequenceIntensionId)
                .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.ConsequenceIntension)
            .WithMany(x => x.ConsequenceIntensionDescriptions)
            .HasForeignKey(x => x.ConsequenceIntensionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}