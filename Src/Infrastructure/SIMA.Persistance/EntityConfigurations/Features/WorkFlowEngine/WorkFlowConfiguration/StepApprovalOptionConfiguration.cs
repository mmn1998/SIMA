using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.StepApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration
{
    public class StepApprovalOptionConfiguration : IEntityTypeConfiguration<StepApprovalOption>
    {
        public void Configure(EntityTypeBuilder<StepApprovalOption> entity)
        {
            entity.ToTable("StepApprovalOption", "Project");

            entity.Property(x => x.Id)
             .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new StepApprovalOptionId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(x => x.Step)
                .WithMany(x => x.StepApprovalOptions)
                .HasForeignKey(x => x.StepId);

            entity.HasOne(x => x.ApprovalOption)
                .WithMany(x => x.StepApprovalOptions)
                .HasForeignKey(x => x.ApprovalOptionId);
        }
    }
}
