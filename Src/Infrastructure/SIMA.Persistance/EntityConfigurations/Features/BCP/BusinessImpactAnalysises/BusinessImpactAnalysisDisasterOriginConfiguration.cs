using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;

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
        entity.Property(i => i.Description).HasColumnType("nvarchar(MAX)").IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConsequenceId)
            .HasConversion(
            x => x.Value,
            x => new ConsequenceId(x)
            );
        entity.HasOne(x => x.Consequence)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.ConsequenceId);
        
        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(
            x => x.Value,
            x => new BusinessImpactAnalysisId(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
        
        entity.Property(x => x.HappeningPossibilityId)
            .HasConversion(
            x => x.Value,
            x => new HappeningPossibilityId(x)
            );
        entity.HasOne(x => x.HappeningPossibility)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.HappeningPossibilityId);
        
        entity.Property(x => x.RecoveryPointObjectiveId)
            .HasConversion(
            x => x.Value,
            x => new RecoveryPointObjectiveId(x)
            );
        entity.HasOne(x => x.RecoveryPointObjective)
            .WithMany(x => x.BusinessImpactAnalysisDisasterOrigins)
            .HasForeignKey(x => x.RecoveryPointObjectiveId);
        
        
    }
}