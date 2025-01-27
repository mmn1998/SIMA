using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class ResponsibleAnswerTypeConfiguration : IEntityTypeConfiguration<ResponsibleAnswerType>
{
    public void Configure(EntityTypeBuilder<ResponsibleAnswerType> entity)
    {
        entity.ToTable("ResponsibleAnswerType", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ResponsibleAnswerTypeId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
    }
}