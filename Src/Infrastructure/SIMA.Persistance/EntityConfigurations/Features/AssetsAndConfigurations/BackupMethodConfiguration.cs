using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.AssetsAndConfigurations;

public class BackupMethodConfiguration: IEntityTypeConfiguration<BackupMethod>
{
 
    public void Configure(EntityTypeBuilder<BackupMethod> entity)
    {
        entity.ToTable("BackupMethod", "Asset");

        
        entity.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new BackupMethodId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
    }
}