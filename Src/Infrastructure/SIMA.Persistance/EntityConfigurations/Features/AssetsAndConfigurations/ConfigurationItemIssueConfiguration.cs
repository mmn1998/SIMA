using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemIssueConfiguration : IEntityTypeConfiguration<ConfigurationItemIssue>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemIssue> entity)
    {
        entity.ToTable("ConfigurationItemIssue", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemIssueId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(x => x.ConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemIssues)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.IssueId)
          .HasConversion(
           v => v.Value,
           v => new IssueId(v));

        entity.HasOne(d => d.Issue)
            .WithMany(d => d.ConfigurationItemIssues)
            .HasForeignKey(d => d.IssueId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
