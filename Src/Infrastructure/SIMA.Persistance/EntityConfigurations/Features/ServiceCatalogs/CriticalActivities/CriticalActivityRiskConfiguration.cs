using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.CriticalActivities;

public class CriticalActivityRiskConfiguration : IEntityTypeConfiguration<CriticalActivityRisk>
{
    public void Configure(EntityTypeBuilder<CriticalActivityRisk> entity)
    {
        entity.ToTable("CriticalActivityRisk", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasColumnName("Id")
    .HasConversion(
        v => v.Value,
        v => new CriticalActivityRiskId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();


        entity.Property(x => x.CriticalActivityId)
            .HasConversion(x => x.Value, x => new CriticalActivityId(x));

        entity.HasOne(x => x.CriticalActivity)
            .WithMany(x => x.CriticalActivityRisks)
            .HasForeignKey(x => x.CriticalActivityId);
    }
}
