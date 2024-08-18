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
        entity.Property(x => x.ConfigurationItemVersioningId)
          .HasConversion(
           v => v.Value,
           v => new ConfigurationItemVersioningId(v));

        entity.HasOne(d => d.ConfigurationItemVersioning)
            .WithMany(d => d.ConfigurationItemIssues)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
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
