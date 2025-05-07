using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.ServiceCatalogs.Channel;

public class ChannelResponsibleConfiguration : IEntityTypeConfiguration<ChannelResponsible>
{
    public void Configure(EntityTypeBuilder<ChannelResponsible> entity)
    {
        entity.ToTable("ChannelResponsible", "ServiceCatalog");

        entity.Property(x => x.Id)
            .HasColumnName("Id")
            .HasConversion(
                v => v.Value,
                v => new ChannelResponsibleId(v))
            .ValueGeneratedNever();

        entity.HasKey(e => e.Id);

        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");

        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ChannelId)
            .HasConversion(x => x.Value, x => new ChannelId(x));

        entity.HasOne(x => x.Channel)
            .WithMany(x => x.ChannelResponsibles)
            .HasForeignKey(x => x.ChannelId);


        entity.Property(x => x.ResponsibleId)
       .HasConversion(x => x.Value, x => new StaffId(x));

        entity.HasOne(x => x.Responsible)
            .WithMany(x => x.ChannelResponsibles)
            .HasForeignKey(x => x.ResponsibleId);


        entity.Property(x => x.ResponsibleTypeId)
      .HasConversion(x => x.Value, x => new ResponsibleTypeId(x));

        entity.HasOne(x => x.ResponsibleType)
            .WithMany(x => x.ChannelResponsibles)
            .HasForeignKey(x => x.ResponsibleTypeId);

        entity.Property(x => x.DepartmentId)
.HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Department)
            .WithMany(x => x.ChannelResponsibles)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);


        entity.Property(x => x.BranchId)
        .HasConversion(x => x.Value, x => new(x));

        entity.HasOne(x => x.Branch)
            .WithMany(x => x.ChannelResponsibles)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        ;
    }
}
