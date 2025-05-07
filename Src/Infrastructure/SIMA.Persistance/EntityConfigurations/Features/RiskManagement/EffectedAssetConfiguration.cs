using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.RiskManagement
{
    public class EffectedAssetConfiguration : IEntityTypeConfiguration<EffectedAsset>
    {
        public void Configure(EntityTypeBuilder<EffectedAsset> entity)
        {
            entity.ToTable("EffectedAsset", "RiskManagement");


            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new EffectedAssetId(v)).ValueGeneratedNever();
            entity.HasKey(i => i.Id);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Risk).WithMany(p => p.EffectedAssets)
                .HasForeignKey(d => d.RiskId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.Property(x => x.AssetId)
                .HasConversion(
                v => v.Value,
                v => new(v)
                );

            entity.HasOne(d => d.Asset).WithMany(p => p.EffectedAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

