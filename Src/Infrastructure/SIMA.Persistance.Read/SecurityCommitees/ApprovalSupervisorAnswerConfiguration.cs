using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.ValueObjects;

namespace SIMA.Persistance.Read.SecurityCommitees;

public class ApprovalSupervisorAnswerConfiguration : IEntityTypeConfiguration<ApprovalSupervisorAnswer>
{
    public void Configure(EntityTypeBuilder<ApprovalSupervisorAnswer> entity)
    {
        entity.ToTable("ApprovalSupervisorAnswer", "SecurityCommitee");
        entity.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ApprovalSupervisorAnswerId(v));
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
            .WithMany(x => x.ApprovalSupervisorAnswers)
            .HasForeignKey(x => x.ApprovalId);

        entity.Property(x => x.SupervisorAnswerTypeId)
            .HasConversion(x => x.Value, v => new SupervisorAnswerTypeId(v));
        entity.HasOne(x => x.SupervisorAnswerType)
            .WithMany(x => x.ApprovalSupervisorAnswers)
            .HasForeignKey(x => x.SupervisorAnswerTypeId);

    }
}
