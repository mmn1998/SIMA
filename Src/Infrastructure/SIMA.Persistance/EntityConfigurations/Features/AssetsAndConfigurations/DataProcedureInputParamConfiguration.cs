using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class DataProcedureInputParamConfiguration : IEntityTypeConfiguration<DataProcedureInputParam>
{
    public void Configure(EntityTypeBuilder<DataProcedureInputParam> entity)
    {
        
        entity.ToTable("DataProcedureInputParam", "AssetAndConfiguration");

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
        entity.Property(e => e.ParentId).HasConversion(v => v.Value, v => new DataProcedureInputParamId(v));
        entity.HasOne(d => d.DataProcedure)
            .WithMany(d => d.DataProcedureInputParam)
            .HasForeignKey(d => d.DataProcedureId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}