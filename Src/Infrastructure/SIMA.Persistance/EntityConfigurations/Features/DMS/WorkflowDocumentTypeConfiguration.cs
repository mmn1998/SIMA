using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.DMS;

public class WorkflowDocumentTypeConfiguration : IEntityTypeConfiguration<WorkflowDocumentType>
{
    public void Configure(EntityTypeBuilder<WorkflowDocumentType> entity)
    {
        entity.ToTable("WorkflowDocumentType", "DMS");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.DocumentTypeId)
            .HasConversion(x => x.Value, v => new(v));
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasOne(x => x.DocumentType)
            .WithMany(x => x.WorkflowDocumentTypes)
                .HasForeignKey(x => x.DocumentTypeId);
    }
}
