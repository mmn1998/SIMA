using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.DMS;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> entity)
    {
        entity.ToTable("Documents", "DMS");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new DocumentId(v));
        entity.Property(x => x.DocumentTypeId)
            .HasConversion(x => x.Value, v => new DocumentTypeId(v));
        entity.Property(x => x.FileExtensionId)
            .HasConversion(x => x.Value, v => new DocumentExtensionId(v));
        entity.Property(x => x.MainAggregateId)
            .HasConversion(x => x.Value, v => new MainAggregateId(v));
        entity.Property(x => x.AttachStepId)
            .HasConversion(x => x.Value, v => new StepId(v));
        entity.HasKey(x => x.Id);
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.Name).HasMaxLength(200).IsUnicode();
        entity.Property(x => x.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.Property(x => x.FileAddress).HasMaxLength(1000).IsUnicode();
        entity.HasOne(x => x.DocumentType)
            .WithMany(x => x.Documents)
                .HasForeignKey(x => x.DocumentTypeId);
        entity.HasOne(x => x.FileExtension)
            .WithMany(x => x.Documents)
                .HasForeignKey(x => x.FileExtensionId)
                ;
        entity.HasOne(x => x.MainAggregate)
            .WithMany(x => x.Documents)
                .HasForeignKey(x => x.MainAggregateId);
        entity.HasOne(x => x.AttachStep)
            .WithMany(x => x.Documents)
                .HasForeignKey(x => x.AttachStepId);

        entity.Property(e => e.StaffSignatureId).HasConversion(v => v.Value, v => new StaffId(v));
        entity.HasOne(d => d.StaffSignature).WithOne(p => p.Signature)
            .HasForeignKey<Document>(d => d.StaffSignatureId);
    }
}
