using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class CorrectiveActionConfiguration : IEntityTypeConfiguration<CorrectiveAction>
    {
        public void Configure(EntityTypeBuilder<CorrectiveAction> entity)
        {
            entity.ToTable("CorrectiveAction", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new CorrectiveActionId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);

            entity.Property(x => x.ActionDescription).IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Risk).WithMany(p => p.CorrectiveActions)
                .HasForeignKey(d => d.RiskId)
                .HasConstraintName("FK_CorrectiveAction_Risk");
           
        }
    }
}
