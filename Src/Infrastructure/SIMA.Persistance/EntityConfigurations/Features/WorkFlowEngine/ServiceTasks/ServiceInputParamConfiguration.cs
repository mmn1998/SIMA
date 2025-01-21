using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ServiceTasks
{
    public class ServiceInputParamConfiguration : IEntityTypeConfiguration<ServiceInputParam>
    {
        public void Configure(EntityTypeBuilder<ServiceInputParam> entity)
        {
            entity.ToTable("ServiceInputParam", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new ServiceInputParamId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();


            entity.Property(x => x.ServiceTaskId)
              .HasConversion(
              v => v.Value,
              v => new ServiceTaskId(v));
            entity.HasOne(d => d.ServiceTask).WithMany(p => p.ServiceInputParams)
                .HasForeignKey(d => d.ServiceTaskId);

            entity.Property(x => x.InputParamId)
             .HasConversion(
             v => v.Value,
             v => new InputParamId(v));
            entity.HasOne(d => d.InputParam).WithMany(p => p.ServiceInputParams)
                .HasForeignKey(d => d.InputParamId);


        }
    }
}
