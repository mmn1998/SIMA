using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class DataProcedureSupportTeamConfiguration : IEntityTypeConfiguration<DataProcedureSupportTeam>
{
    public void Configure(EntityTypeBuilder<DataProcedureSupportTeam> entity)
    {
        entity.ToTable("DataProcedureSupportTeam", "AssetAndConfiguration");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DataProcedureId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.DataProcedure)
            .WithMany(x => x.DataProcedureSupportTeams)
            .HasForeignKey(x => x.DataProcedureId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.StaffId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.Staff)
            .WithMany(x => x.DataProcedureSupportTeams)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.DepartmentId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.Department)
            .WithMany(x => x.DataProcedureSupportTeams)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.BranchId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x => x.Branch)
            .WithMany(x => x.DataProcedureSupportTeams)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
