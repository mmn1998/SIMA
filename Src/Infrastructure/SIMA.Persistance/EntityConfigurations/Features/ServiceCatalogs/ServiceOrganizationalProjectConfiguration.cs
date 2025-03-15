using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs;

public class ServiceOrganizationalProjectConfiguration : IEntityTypeConfiguration<ServiceOrganizationalProject>
{
    public void Configure(EntityTypeBuilder<ServiceOrganizationalProject> entity)
    {
        entity.ToTable("ServiceOrganizationalProject", "ServiceCatalog");
        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(v => v.Value, v => new ServiceOrganizationalProjectId(v))
            .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();



        entity.Property(x => x.ServiceId).HasConversion(
            v => v.Value, v => new ServiceId(v));

        entity.HasOne(x => x.Service)
     .WithMany(p => p.ServiceOrganizationalProjects)
     .HasForeignKey(x => x.ServiceId);
        
        entity.Property(x => x.OrganizationalProjectId).HasConversion(
            v => v.Value, v => new OrganizationalProjectId(v));

        entity.HasOne(x => x.OrganizationalProject)
     .WithMany(p => p.ServiceOrganizationalProjects)
     .HasForeignKey(x => x.OrganizationalProjectId);
    }
}