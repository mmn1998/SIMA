using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ViewLists.Entities;
using SIMA.Domain.Models.Features.Auths.ViewLists.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ViewListConfiguration : IEntityTypeConfiguration<ViewList>
{
    public void Configure(EntityTypeBuilder<ViewList> entity)
    {
        entity.ToTable("ViewList", "Authentication");

        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new ViewId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);
    }
}