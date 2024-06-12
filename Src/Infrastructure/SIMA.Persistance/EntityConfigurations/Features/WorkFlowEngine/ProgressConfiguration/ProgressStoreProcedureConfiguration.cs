using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ProgressConfiguration;

public class ProgressStoreProcedureConfiguration : IEntityTypeConfiguration<ProgressStoreProcedure>
{
    public void Configure(EntityTypeBuilder<ProgressStoreProcedure> entity)
    {
        entity.ToTable("ProgressStoreProcedure", "Project");

        entity.Property(x => x.Id)
        .HasColumnName("Id")
        .HasConversion(
         v => v.Value,
         v => new ProgressStoreProcedureId(v))
        .ValueGeneratedNever();
        entity.Property(e => e.ActiveStatusId);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ProgressId)
           .HasConversion(
           v => v.Value,
           v => new ProgressId(v));
        ;
        entity.HasOne(d => d.Progress).WithMany(p => p.ProgressStoreProcedures)
                .HasForeignKey(d => d.ProgressId)
                .OnDelete(DeleteBehavior.ClientSetNull);
    }
}