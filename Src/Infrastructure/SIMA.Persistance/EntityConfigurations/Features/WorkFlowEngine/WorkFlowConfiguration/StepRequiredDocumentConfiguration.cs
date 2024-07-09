using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

public class StepRequiredDocumentConfiguration : IEntityTypeConfiguration<StepRequiredDocument>
{
    public void Configure(EntityTypeBuilder<StepRequiredDocument> entity)
    {
        entity.ToTable("StepRequiredDocument", "Project");


        entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
         v => v.Value,
         v => new StepRequiredDocumentId(v))
        .ValueGeneratedNever();

        entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.StepId)
            .HasConversion(v => v.Value, v => new StepId(v));
        entity.HasOne(d => d.Step).WithMany(p => p.StepRequiredDocuments)
    .HasForeignKey(d => d.StepId);

        entity.Property(x => x.DocumentTypeId)
           .HasConversion(v => v.Value, v => new DocumentTypeId(v));
        entity.HasOne(d => d.DocumentType).WithMany(p => p.StepRequiredDocuments)
    .HasForeignKey(d => d.DocumentTypeId);

    }
}