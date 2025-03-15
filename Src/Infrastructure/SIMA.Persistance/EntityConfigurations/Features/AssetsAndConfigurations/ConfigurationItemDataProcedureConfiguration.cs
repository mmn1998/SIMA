using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemDataProcedureConfiguration : IEntityTypeConfiguration<ConfigurationItemDataProcedure>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemDataProcedure> entity)
    {
        entity.ToTable("ConfigurationItemDataProcedures", "Asset");
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
        
        
        entity.HasOne(d => d.DataProcedure)
            .WithMany(d => d.ConfigurationItemDataProcedures)
            .HasForeignKey(d => d.DataProcedureId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        
        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemDataProcedures)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}