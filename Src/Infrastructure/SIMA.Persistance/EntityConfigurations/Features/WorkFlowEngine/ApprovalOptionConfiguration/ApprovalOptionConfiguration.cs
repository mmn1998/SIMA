using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ApprovalOptionConfiguration
{
    public class ApprovalOptionConfiguration : IEntityTypeConfiguration<ApprovalOption>
    {
        public void Configure(EntityTypeBuilder<ApprovalOption> entity)
        {
            entity.ToTable("ApprovalOption", "Project");

            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new ApprovalOptionId(v))
            .ValueGeneratedNever();

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Code).HasMaxLength(20);
        }
    }
}
