using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class PreventiveActionConfiguration : IEntityTypeConfiguration<PreventiveAction>
    {
        public void Configure(EntityTypeBuilder<PreventiveAction> entity)
        {
            entity.ToTable("PreventiveAction", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new PreventiveActionId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(x => x.ActionDescription).IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Risk).WithMany(p => p.PreventiveActions)
                .HasForeignKey(d => d.RiskId)
                .HasConstraintName("FK_PreventiveAction_Risk");

        }
    }
}
