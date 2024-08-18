using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemDocumentConfiguration : IEntityTypeConfiguration<ConfigurationItemDocument>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemDocument> entity)
    {
        entity.ToTable("ConfigurationItemDocument", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemDocumentId(v)).ValueGeneratedNever();
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
            .WithMany(d => d.ConfigurationItemDocuments)
            .HasForeignKey(d => d.ConfigurationItemVersioningId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DocumentId)
          .HasConversion(
           v => v.Value,
           v => new DocumentId(v));

        entity.HasOne(d => d.Document)
            .WithMany(d => d.ConfigurationItemDocuments)
            .HasForeignKey(d => d.DocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
