using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.TrustyDrafts;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> entity)
    {
        entity.ToTable("Resource", "TrustyDraft");


        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ResourceId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);

        entity.Property(x => x.AccountNumber).HasMaxLength(20);
        entity.Property(x => x.Code).HasMaxLength(20);
        entity.HasIndex(x => x.Code).IsUnique();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();


        entity.Property(e => e.ParentId)
            .HasConversion
                (v => v.Value,
                v => new(v));

        entity.Property(e => e.AccountTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.AccountType)
            .WithMany(x => x.Resources)
            .HasForeignKey(x => x.AccountTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.BrokerId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.Broker)
            .WithMany(x => x.Resources)
            .HasForeignKey(x => x.BrokerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.Property(e => e.CurrencyTypeId)
            .HasConversion
                (v => v.Value,
                v => new(v));
        entity.HasOne(x => x.CurrencyType)
            .WithMany(x => x.Resources)
            .HasForeignKey(x => x.CurrencyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}