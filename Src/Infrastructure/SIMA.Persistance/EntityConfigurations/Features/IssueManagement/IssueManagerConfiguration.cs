using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.IssueManagement
{
    public class IssueManagerConfiguration : IEntityTypeConfiguration<IssueManager>
    {
        public void Configure(EntityTypeBuilder<IssueManager> entity)
        {
            entity.ToTable("IssueManager", "IssueManagement");
            entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
            v => v.Value,
            v => new IssueManagerId(v))
        .ValueGeneratedNever();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
                            .HasDefaultValueSql("(getdate())")
                            .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                        .IsRowVersion()
                        .IsConcurrencyToken();
            entity.Property(x => x.IssueId)
                   .HasConversion(
                    v => v.Value,
                    v => new IssueId(v));
            entity.Property(x => x.UserId)
                   .HasConversion(
                    v => v.Value,
                    v => new UserId(v));

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueManagers)
                    .HasForeignKey(d => d.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.IssueManagers)
                    .HasForeignKey(d => d.UserId);
        }
    }
}
