using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class DataProcedureOutputParamConfiguration : IEntityTypeConfiguration<DataProcedureOutputParam>
{
    public void Configure(EntityTypeBuilder<DataProcedureOutputParam> entity)
    {
        entity.ToTable("DataProcedureOutputParam", "AssetAndConfiguration");

        entity.HasKey(e => e.Id);
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new DataProcedureOutputParamId(v));
        entity.HasOne(d => d.DataProcedure)
            .WithMany(d => d.DataProcedureOutputParam)
            .HasForeignKey(d => d.DataProcedureId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}