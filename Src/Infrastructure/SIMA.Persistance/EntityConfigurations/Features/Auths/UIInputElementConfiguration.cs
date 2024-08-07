using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
    public class UIInputElementConfiguration : IEntityTypeConfiguration<UIInputElement>
    {
        public void Configure(EntityTypeBuilder<UIInputElement> entity)
        {


            entity.ToTable("UIInputElement", "Basic");

            entity.Property(x => x.Id)
                .HasConversion(
                    v => v.Value,
                    v => new UIInputElementId(v))
                .ValueGeneratedNever();
            entity.HasIndex(x => x.Code).IsUnique();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HasInputInEachRecord)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsMultiSelect)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsSingleSelect)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Code).IsUnicode(false).HasMaxLength(20);
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
