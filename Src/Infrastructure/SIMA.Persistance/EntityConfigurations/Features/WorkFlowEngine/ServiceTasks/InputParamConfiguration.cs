using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.ValueObjects;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ServiceTasks
{
    public class InputParamConfiguration : IEntityTypeConfiguration<InputParam>
    {
        public void Configure(EntityTypeBuilder<InputParam> entity)
        {
            entity.ToTable("InputParam", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new InputParamId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();


            entity.Property(x => x.DataTypeId)
              .HasConversion(
              v => v.Value,
              v => new DataTypeId(v));
            entity.HasOne(d => d.DataType).WithMany(p => p.InputParams)
                .HasForeignKey(d => d.DataTypeId);

            
        }
    }
}
