using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ProgressConfiguration;

public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> entity)
    {
        //var boolCharConverter =
        //        new ValueConverter<bool, string>(v => v ? "1" : "0", v => v == "X");

        entity.ToTable("Progress", "Project");

        entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
         v => v.Value,
         v => new ProgressId(v))
        .ValueGeneratedNever();
        entity.Property(e => e.ActiveStatusId);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode();
        //entity.Property(x => x.HasStoreProcedure)
        //     .HasConversion(dbVarCharNullableToBoolNullableConverter);
        entity.Property(x => x.HasStoreProcedure).HasMaxLength(1);
        entity.Property(x => x.WorkFlowId)
           .HasConversion(
           v => v.Value,
           v => new WorkFlowId(v));

        entity.Property(x => x.StateId)
               .HasConversion(
               v => v.Value,
               v => new StateId(v));

        entity.HasOne(d => d.WorkFlow).WithMany(p => p.Progresses)
            .HasForeignKey(d => d.WorkFlowId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            ;
        entity.HasOne(d => d.State).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        
    }
    //public static ValueConverter<bool?, string> dbVarCharNullableToBoolNullableConverter = new ValueConverter<bool?, string>(
    //v => v == true ? "1" : v == false ? "0" : null,
    //v => v == "1" ? true : false);
}