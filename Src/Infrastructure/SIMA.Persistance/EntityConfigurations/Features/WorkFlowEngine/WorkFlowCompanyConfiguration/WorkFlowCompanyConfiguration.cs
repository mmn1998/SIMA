using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.ValueObject;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowCompanyConfiguration
{
    public class WorkFlowCompanyConfiguration : IEntityTypeConfiguration<WorkFlowCompany>
    {
        public void Configure(EntityTypeBuilder<WorkFlowCompany> entity)
        {
            entity.ToTable("WorkFlowCompany", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new WorkFlowCompanyId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.WorkFlowId)
              .HasConversion(
              v => v.Value,
              v => new WorkFlowId(v));
            entity.HasOne(d => d.WorkFlow).WithMany(p => p.WorkFlowCompanies)
                .HasForeignKey(d => d.WorkFlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                ;
        }
    }
}
