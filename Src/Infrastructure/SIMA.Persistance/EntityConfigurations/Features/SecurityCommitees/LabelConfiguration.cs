using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.ValueObject;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> entity)
    {
        entity.ToTable("Label", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new LabelId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
    }
}
