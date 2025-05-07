using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.DMS;

public class WorkflowDocumentExtensionConfiguration : IEntityTypeConfiguration<WorkflowDocumentExtension>
{
    public void Configure(EntityTypeBuilder<WorkflowDocumentExtension> entity)
    {
        entity.ToTable("WorkflowDocumentExtension", "DMS");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.DocumentExtensionId)
            .HasConversion(x => x.Value, v => new(v));
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasOne(x => x.DocumentExtension)
            .WithMany(x => x.WorkflowDocumentExtensions)
                .HasForeignKey(x => x.DocumentExtensionId);
    }
}
