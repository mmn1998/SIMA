using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.DataTypes.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ProgressConfiguration;

public class ProgressStoreProcedureParamConfiguration : IEntityTypeConfiguration<ProgressStoreProcedureParam>
{
    public void Configure(EntityTypeBuilder<ProgressStoreProcedureParam> entity)
    {
        entity.ToTable("ProgressStoreProcedureParam", "Project");

        entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
         v => v.Value,
         v => new ProgressStoreProcedureParamId(v))
        .ValueGeneratedNever();
        entity.Property(e => e.ActiveStatusId);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.IsRequired).HasMaxLength(1);
        entity.Property(e => e.Name).HasMaxLength(200);

        entity.Property(x => x.ProgressStoreProcedureId)
           .HasConversion(
           v => v.Value,
           v => new ProgressStoreProcedureId(v));
        ;
        entity.HasOne(d => d.ProgressStoreProcedure).WithMany(p => p.ProgressStoreProcedureParams)
                .HasForeignKey(d => d.ProgressStoreProcedureId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.DataTypeId)
           .HasConversion(
           v => v.Value,
           v => new DataTypeId(v));
        ;
        entity.HasOne(d => d.DataType).WithMany(p => p.ProgressStoreProcedureParams)
                .HasForeignKey(d => d.DataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
