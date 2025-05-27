using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskValueConfiguration : IEntityTypeConfiguration<RiskValue>
{
    public void Configure(EntityTypeBuilder<RiskValue> entity)
    {
        entity.ToTable("RiskValue", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Color).IsRequired().HasMaxLength(10);
        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        //entity.Property(x => x.StrategyId)
        //    .HasConversion(x => x.Value,
        //    x => new(x));
        //entity.HasOne(x => x.Strategy)
        //    .WithMany(x => x.RiskValues)
        //    .HasForeignKey(x => x.StrategyId)
        //    .OnDelete(DeleteBehavior.ClientSetNull);
    }
}