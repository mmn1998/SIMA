using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.BCP.RecoveryOptionPriorities
{
    public class RecoveryOptionPriorityConfiguration : IEntityTypeConfiguration<RecoveryOptionPriority>
    {
        public void Configure(EntityTypeBuilder<RecoveryOptionPriority> entity)
        {
            entity.ToTable("RecoveryOptionPriority", "BCP");
            entity.HasIndex(e => e.Code).IsUnique();
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new RecoveryOptionPriorityId(v)).ValueGeneratedNever();
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
