using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.StrategyTypes
{
    public class StrategyTypeConfiguration : IEntityTypeConfiguration<StrategyType>
    {
        public void Configure(EntityTypeBuilder<StrategyType> entity)
        {
            entity.ToTable("StrategyType", "BCP");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new StrategyTypeId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
