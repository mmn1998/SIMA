using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessImpactAnalysises;

public class BusinessImpactAnalysisAnnouncementConfiguration : IEntityTypeConfiguration<BusinessImpactAnalysisAnnouncement>
{
    public void Configure(EntityTypeBuilder<BusinessImpactAnalysisAnnouncement> entity)
    {
        entity.ToTable("BCP", "BusinessImpactAnalysisAnnouncement");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new BusinessImpactAnalysisAnnouncementId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.BusinessImpactAnalysisId)
            .HasConversion(
            x => x.Value,
            x => new BusinessImpactAnalysisId(x)
            );
        entity.HasOne(x => x.BusinessImpactAnalysis)
            .WithMany(x => x.BusinessImpactAnalysisAnnouncements)
            .HasForeignKey(x => x.BusinessImpactAnalysisId);
        
        entity.Property(x => x.StaffId)
            .HasConversion(
            x => x.Value,
            x => new StaffId(x)
            );
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.BusinessImpactAnalysisAnnouncements)
            .HasForeignKey(x => x.StaffId);
    }
}
