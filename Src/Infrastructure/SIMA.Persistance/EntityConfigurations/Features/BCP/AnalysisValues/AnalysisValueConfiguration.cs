using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.AnalysisValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.AnalysisValues;

public class AnalysisValueConfiguration : IEntityTypeConfiguration<AnalysisValue>
{
    public void Configure(EntityTypeBuilder<AnalysisValue> entity)
    {
        entity.ToTable("AnalysisValue", "BCP");
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
            .WithMany(x => x.AnalysisValues)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ConsequenceIntensionId)
                .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.ConsequenceIntension)
            .WithMany(x => x.AnalysisValues)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}