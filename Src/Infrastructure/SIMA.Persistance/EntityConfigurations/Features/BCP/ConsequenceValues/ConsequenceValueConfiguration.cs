using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.ConsequenceValues;

public class ConsequenceValueConfiguration : IEntityTypeConfiguration<ConsequenceValue>
{
    public void Configure(EntityTypeBuilder<ConsequenceValue> entity)
    {
        entity.ToTable("ConsequenceValue", "BCP");
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
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
        entity.HasOne(x => x.Consequence)
            .WithMany(x => x.ConsequenceValues)
            .HasForeignKey(x => x.ConsequenceId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.OriginId)
                .HasConversion(
                x => x.Value,
                x => new(x)
                );
        entity.HasOne(x => x.Origin)
            .WithMany(x => x.ConsequenceValues)
            .HasForeignKey(x => x.OriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}