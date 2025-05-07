using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement;

public class RiskStaffConfiguration : IEntityTypeConfiguration<RiskStaff>
{
    public void Configure(EntityTypeBuilder<RiskStaff> entity)
    {
        entity.ToTable("RiskStaff", "RiskManagement");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.RiskId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Risk)
            .WithMany(x => x.RiskStaffs)
            .HasForeignKey(x => x.RiskId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.StaffId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.RiskStaffs)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.ResponsilbeTypeId)
            .HasConversion(
            x => x.Value,
            x => new(x)
            );
        entity.HasOne(x => x.ResponsilbeType)
            .WithMany()
            .HasForeignKey(x => x.ResponsilbeTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}