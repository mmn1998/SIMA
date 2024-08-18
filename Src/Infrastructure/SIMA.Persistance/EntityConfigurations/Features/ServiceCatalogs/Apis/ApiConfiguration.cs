using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Apis;

public class ApiConfiguration : IEntityTypeConfiguration<Api>
{
    public void Configure(EntityTypeBuilder<Api> entity)
    {
        entity.ToTable("Api", "ServiceCatalog");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ApiId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.PortNumber).HasMaxLength(5);
        entity.Property(e => e.IpAddress).HasMaxLength(40);
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.ApiTypeId)
            .HasConversion(x => x.Value, x => new ApiTypeId(x));
        entity.HasOne(x=>x.ApiType)
            .WithMany(x=>x.Apis)
            .HasForeignKey(x=>x.ApiTypeId);

        entity.Property(x => x.ApiAuthenticationMethodId)
            .HasConversion(x => x.Value, x => new ApiAuthenticationMethodId(x));
        entity.HasOne(x=>x.ApiAuthenticationMethod)
            .WithMany(x=>x.Apis)
            .HasForeignKey(x=>x.ApiAuthenticationMethodId);

        entity.Property(x => x.NetworkProtocolId)
            .HasConversion(x => x.Value, x => new NetworkProtocolId(x));
        entity.HasOne(x=>x.NetworkProtocol)
            .WithMany(x=>x.Apis)
            .HasForeignKey(x=>x.NetworkProtocolId);
       
        entity.Property(x => x.OwnerDepartmentId)
            .HasConversion(x => x.Value, x => new DepartmentId(x));
        entity.HasOne(x=>x.OwnerDepartment)
            .WithMany(x=>x.OwnerApis)
            .HasForeignKey(x=>x.OwnerDepartmentId);

        entity.Property(x => x.OwnerResponsibleId)
            .HasConversion(x => x.Value, x => new StaffId(x));
        entity.HasOne(x=>x.OwnerResponsible)
            .WithMany(x=>x.ResponsibleApis)
            .HasForeignKey(x=>x.OwnerResponsibleId);
        
        entity.Property(x => x.ApiMethodActionId)
            .HasConversion(x => x.Value, x => new ApiMethodActionId(x));
        entity.HasOne(x=>x.ApiMethodAction)
            .WithMany(x=>x.Apis)
            .HasForeignKey(x=>x.ApiMethodActionId);
    }
}