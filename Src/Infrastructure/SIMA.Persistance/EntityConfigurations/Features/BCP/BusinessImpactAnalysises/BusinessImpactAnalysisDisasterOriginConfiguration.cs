using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisDisasterOriginConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisDisasterOrigin>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisDisasterOrigin> entity)
    {
        entity.ToTable("BusinessImpactAnalysisDisasterOrigin", "BCP");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisDisasterOriginId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConsequenceId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Consequence)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.ConsequenceId);

        entity.Property(x => x.OriginId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Origin)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.OriginId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
        
        entity.Property(x => x.ConsequenceIntensionId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.ConsequenceIntension)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins);




    }
}