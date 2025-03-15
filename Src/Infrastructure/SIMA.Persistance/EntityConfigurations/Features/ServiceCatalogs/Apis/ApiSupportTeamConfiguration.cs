using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiSupportTeamConfiguration : IEntityTypeConfiguration<ApiSupportTeam>
{
    public void Configure(EntityTypeBuilder<ApiSupportTeam> entity)
    {
        entity.ToTable("ApiSupportTeam", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiSupportTeamId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ApiId)
            .HasConversion(x => x.Value, x => new ApiId(x));
        entity.HasOne(x=>x.Api)
            .WithMany(x=>x.ApiSupportTeams)
            .HasForeignKey(x=>x.ApiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.StaffId)
            .HasConversion(x => x.Value, x => new StaffId(x));
        entity.HasOne(x=>x.Staff)
            .WithMany(x=>x.ApiSupportTeams)
            .HasForeignKey(x=>x.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.DepartmentId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x=>x.Department)
            .WithMany(x=>x.ApiSupportTeams)
            .HasForeignKey(x=>x.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.BranchId)
            .HasConversion(x => x.Value, x => new(x));
        entity.HasOne(x=>x.Branch)
            .WithMany(x=>x.ApiSupportTeams)
            .HasForeignKey(x=>x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
