using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.ActionTypeConfiguration
{
    public class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
    {
        public void Configure(EntityTypeBuilder<ActionType> entity)
        {

            entity.ToTable("ActionType", "Project");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
         .HasColumnName("Id")
         .HasConversion(
             v => v.Value,
             v => new ActionTypeId(v))
         .ValueGeneratedNever();
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ActiveStatusId).HasColumnName("ActiveStatusID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode();
        }
    }
}
