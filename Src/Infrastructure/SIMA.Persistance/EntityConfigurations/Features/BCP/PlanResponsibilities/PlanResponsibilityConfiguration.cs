using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.PlanResponsibilities
{
    public class PlanResponsibilityConfiguration : IEntityTypeConfiguration<PlanResponsibility>
    {
        public void Configure(EntityTypeBuilder<PlanResponsibility> entity)
        {
            entity.ToTable("PlanResponsibility", "BCP");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new PlanResponsibilityId(v)).ValueGeneratedNever();
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
