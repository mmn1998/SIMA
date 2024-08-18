using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.SecurityCommitees;

public class ApprovalResponsibleAnswerConfiguration : IEntityTypeConfiguration<ApprovalResponsibleAnswer>
{
    public void Configure(EntityTypeBuilder<ApprovalResponsibleAnswer> entity)
    {
        entity.ToTable("ApprovalResponsibleAnswer", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ApprovalResponsibleAnswerId(v));
        entity.HasKey(x => x.Id);
        entity.Property(x => x.CreatedAt)
                         .HasDefaultValueSql("(getdate())")
                         .HasColumnType("datetime");
        entity.Property(x => x.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ApprovalId)
            .HasConversion(x => x.Value, v => new ApprovalId(v));
        entity.HasOne(x => x.Approval)
            .WithMany(x => x.ApprovalResponsibleAnswers)
            .HasForeignKey(x => x.ApprovalId);

        entity.Property(x => x.ResponsibleAnswerTypeId)
            .HasConversion(x => x.Value, v => new ResponsibleAnswerTypeId(v));
        entity.HasOne(x => x.ResponsibleAnswerType)
            .WithMany(x => x.ApprovalResponsibleAnswers)
            .HasForeignKey(x => x.ResponsibleAnswerTypeId);
    }
}
