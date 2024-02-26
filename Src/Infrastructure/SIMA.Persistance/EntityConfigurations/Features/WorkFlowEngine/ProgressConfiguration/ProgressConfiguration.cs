using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ProgressConfiguration
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> entity)
        {

            entity.ToTable("Progress", "Project");

            entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
             v => v.Value,
             v => new ProgressId(v))
            .ValueGeneratedNever();
            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode();
            entity.Property(x => x.WorkFlowId)
               .HasConversion(
               v => v.Value,
               v => new WorkFlowId(v));
            entity.HasOne(d => d.WorkFlow).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.WorkFlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                ;
        }
    }
}
