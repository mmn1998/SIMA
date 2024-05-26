using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysis>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysis> entity)
    {
        entity.ToTable("BCP", "BusinessImpactAnalysis");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ImportanceDegreeId)
            .HasConversion(
            x => x.Value,
            x => new ImportanceDegreeId(x)
            );
        entity.HasOne(x => x.ImportanceDegree)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.ImportanceDegreeId);
        
        entity.Property(x => x.ServicePriorityId)
            .HasConversion(
            x => x.Value,
            x => new ServicePriorityId(x)
            );
        entity.HasOne(x => x.ServicePriority)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.ServicePriorityId);
        
        entity.Property(x => x.BackupPeriodId)
            .HasConversion(
            x => x.Value,
            x => new BackupPeriodId(x)
            );
        entity.HasOne(x => x.BackupPeriod)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.BackupPeriodId);
        
        entity.Property(x => x.MaximumAcceptableOutageId)
            .HasConversion(
            x => x.Value,
            x => new MaximumAcceptableOutageId(x)
            );
        entity.HasOne(x => x.MaximumAcceptableOutage)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.MaximumAcceptableOutageId);
        
        entity.Property(x => x.ConsequenceId)
            .HasConversion(
            x => x.Value,
            x => new ConsequenceId(x)
            );
        entity.HasOne(x => x.Consequence)
            .WithMany(x => x.BusinessImpactAnalyses)
            .HasForeignKey(x => x.ConsequenceId);
    }
}