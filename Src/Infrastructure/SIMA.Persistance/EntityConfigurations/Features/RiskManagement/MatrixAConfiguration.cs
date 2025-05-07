using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class MatrixAConfiguration : IEntityTypeConfiguration<MatrixA>
{
    public void Configure(EntityTypeBuilder<MatrixA> entity)
    {
        entity.ToTable("MatrixA", "RiskManagement");

        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(e => e.Code).IsRequired().HasMaxLength(20);

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.TriggerStatusId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.TriggerStatus)
            .WithMany(x => x.MatrixAs)
            .HasForeignKey(x => x.TriggerStatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.MatrixAValueId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.MatrixAValue)
            .WithMany(x => x.MatrixAs)
            .HasForeignKey(x => x.MatrixAValueId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.UseVulnerabilityId)
            .HasConversion(x => x.Value,
            x => new(x));
        entity.HasOne(x => x.UseVulnerability)
            .WithMany(x => x.MatrixAs)
            .HasForeignKey(x => x.UseVulnerabilityId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}