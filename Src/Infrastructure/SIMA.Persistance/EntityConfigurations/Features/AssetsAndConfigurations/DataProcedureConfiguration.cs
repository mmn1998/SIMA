using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class DataProcedureConfiguration : IEntityTypeConfiguration<DataProcedure>
{
    public void Configure(EntityTypeBuilder<DataProcedure> entity)
    {
        entity.ToTable("DataProcedure", "AssetAndConfiguration");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
        
        entity.HasOne(d => d.Database)
            .WithMany(d => d.DataProcedure)
            .HasForeignKey(d => d.DatabaseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        
        entity.HasOne(d => d.DataProcedureType)
            .WithMany(d => d.DataProcedure)
            .HasForeignKey(d => d.DataProcedureTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}