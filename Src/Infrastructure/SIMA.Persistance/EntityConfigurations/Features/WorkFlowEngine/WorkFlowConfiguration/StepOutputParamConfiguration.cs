using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

public class StepOutputParamConfiguration : IEntityTypeConfiguration<StepOutputParam>
{
    public void Configure(EntityTypeBuilder<StepOutputParam> entity)
    {
        entity.ToTable("StepOutputParam", "Project");


        entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
         v => v.Value,
         v => new StepOutputParamId(v))
        .ValueGeneratedNever();

        entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.IsRequired).HasMaxLength(1);


        entity.Property(x => x.StepId)
            .HasConversion(
            v => v.Value,
            v => new StepId(v));

        entity.Property(x => x.DataTypeId)
           .HasConversion(
           v => v.Value,
           v => new DataTypeId(v));


        entity.HasOne(s => s.Step)
            .WithMany(p => p.StepOutputParams)
            .HasForeignKey(p => p.StepId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(s => s.DataType)
            .WithMany(p => p.StepOutputParams)
            .HasForeignKey(p => p.DataTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
