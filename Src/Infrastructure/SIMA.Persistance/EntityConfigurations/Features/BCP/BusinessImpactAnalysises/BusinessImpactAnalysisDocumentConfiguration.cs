using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisDocumentConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisDocument>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisDocument> entity)
    {
        entity.ToTable("BusinessImpactAnalysisDocument", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisDocumentId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(
            x => x.Value,
            x => new BusinessImpactAnalysisId(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisDocuments)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
        
        entity.Property(x => x.DocumentId)
            .HasConversion(
            x => x.Value,
            x => new DocumentId(x)
            );
        entity.HasOne(x => x.Document)
            .WithMany(x => x.BusinessImpactAnalysisDocuments)
            .HasForeignKey(x => x.DocumentId);
    }
}
