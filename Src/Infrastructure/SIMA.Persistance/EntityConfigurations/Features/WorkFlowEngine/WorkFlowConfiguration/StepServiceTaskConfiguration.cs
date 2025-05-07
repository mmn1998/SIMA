using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration
{
    public class StepServiceTaskConfiguration : IEntityTypeConfiguration<StepServiceTask>
    {
        public void Configure(EntityTypeBuilder<StepServiceTask> entity)
        {
            entity.ToTable("StepServiceTask", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new StepServiceTaskId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

          
            entity.Property(x => x.StepId)
              .HasConversion(
              v => v.Value,
              v => new StepId(v));
            entity.HasOne(d => d.Step).WithMany(p => p.StepServiceTasks)
                .HasForeignKey(d => d.StepId);

            entity.Property(x => x.ServiceTaskId)
             .HasConversion(
             v => v.Value,
             v => new ServiceTaskId(v));
            entity.HasOne(d => d.ServiceTask).WithMany(p => p.StepServiceTasks)
                .HasForeignKey(d => d.ServiceTaskId);

            ;
        }
    }
}
