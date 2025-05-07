using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> entity)
    {
        entity.ToTable("Subject", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new SubjectId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ArchivedBy)
            .HasConversion(x => x.Value, v => new UserId(v));
        entity.HasOne(x => x.Archiver)
            .WithMany(x => x.Subjects)
            .HasForeignKey(x => x.ArchivedBy);
    }
}
