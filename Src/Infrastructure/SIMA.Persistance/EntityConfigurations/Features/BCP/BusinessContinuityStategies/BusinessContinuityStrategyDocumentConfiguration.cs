using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies;

public class BusinessContinuityStrategyDocumentConfiguration : IEntityTypeConfiguration<BusinessContinuityStrategyDocument>
{
    public void Configure(EntityTypeBuilder<BusinessContinuityStrategyDocument> entity)
    {
        entity.ToTable("BCP", "BusinessContinuityStrategyDocument");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessContinuityStrategyDocumentId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessContinuityStategyId)
            .HasConversion(
            x => x.Value,
            x => new BusinessContinuityStrategyId(x)
            );
        entity.HasOne(x => x.BusinessContinuityStategy)
            .WithMany(x => x.BusinessContinuityStrategyDocuments)
            .HasForeignKey(x => x.BusinessContinuityStategyId);
        entity.Property(x => x.DocumentId)
            .HasConversion(
            x => x.Value,
            x => new DocumentId(x)
            );
        entity.HasOne(x => x.Document)
            .WithMany(x => x.BusinessContinuityStrategyDocuments)
            .HasForeignKey(x => x.DocumentId);
    }
}
