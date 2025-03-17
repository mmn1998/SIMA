using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class ConfigurationItemSupportTeamConfiguration : IEntityTypeConfiguration<ConfigurationItemSupportTeam>
{
    public void Configure(EntityTypeBuilder<ConfigurationItemSupportTeam> entity)
    {
        entity.ToTable("ConfigurationItemSupportTeam", "AssetAndConfiguration");
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ConfigurationItemSupportTeamId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();

        entity.Property(x => x.ConfigurationItemId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.ConfigurationItem)
            .WithMany(d => d.ConfigurationItemSupportTeams)
            .HasForeignKey(d => d.ConfigurationItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.MainStaffId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.MainStaff)
            .WithMany(d => d.MainConfigurationItemSupportTeams)
            .HasForeignKey(d => d.MainStaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SubsitutedStaffId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.SubsitutedStaff)
            .WithMany(d => d.SubsitutedConfigurationItemSupportTeams)
            .HasForeignKey(d => d.SubsitutedStaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.MainDepartmentId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.MainDepartment)
            .WithMany(d => d.MainConfigurationItemSupportTeams)
            .HasForeignKey(d => d.MainDepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SubsitutedDepartmentId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.SubsitutedDepartment)
            .WithMany(d => d.SubsitutedConfigurationItemSupportTeams)
            .HasForeignKey(d => d.SubsitutedDepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.MainBranchId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.MainBranch)
            .WithMany(d => d.MainConfigurationItemSupportTeams)
            .HasForeignKey(d => d.MainBranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(x => x.SubsitutedBranchId)
          .HasConversion(
           v => v.Value,
           v => new(v));

        entity.HasOne(d => d.SubsitutedBranch)
            .WithMany(d => d.SubsitutedConfigurationItemSupportTeams)
            .HasForeignKey(d => d.SubsitutedBranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}