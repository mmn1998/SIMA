using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityStategies
{
    public class BusinessContinuityStratgySolutionConfiguration : IEntityTypeConfiguration<BusinessContinuityStratgySolution>
    {
        public void Configure(EntityTypeBuilder<BusinessContinuityStratgySolution> entity)
        {
            entity.ToTable("BusinessContinuityStratgySolution", "BCP");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new BusinessContinuityStratgySolutionId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(x => x.Code).HasMaxLength(20).IsUnicode(false);
            entity.Property(x => x.Title).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.Property(x => x.BusinessContinuityStratgyId)
                .HasConversion(
                x => x.Value,
                x => new BusinessContinuityStrategyId(x)
                );
            entity.HasOne(x => x.BusinessContinuityStratgy)
                .WithMany(x => x.BusinessContinuityStratgySolutions)
                .HasForeignKey(x => x.BusinessContinuityStratgyId);
        }
    }
}
