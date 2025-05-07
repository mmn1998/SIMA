using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ServiceTasks
{
    public class ServiceTaskConfiguration : IEntityTypeConfiguration<ServiceTask>
    {
        public void Configure(EntityTypeBuilder<ServiceTask> entity)
        {
            entity.ToTable("ServiceTask", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new ServiceTaskId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();


            entity.Property(x => x.ApiMethodActionId)
              .HasConversion(
              v => v.Value,
              v => new ApiMethodActionId(v));
            entity.HasOne(d => d.ApiMethodAction).WithMany(p => p.ServiceTasks)
                .HasForeignKey(d => d.ApiMethodActionId);


        }
    }
}
