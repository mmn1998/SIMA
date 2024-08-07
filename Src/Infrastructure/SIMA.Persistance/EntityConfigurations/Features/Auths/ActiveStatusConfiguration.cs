using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

public class ActiveStatusConfiguration : IEntityTypeConfiguration<ActiveStatus>
{
    public void Configure(EntityTypeBuilder<ActiveStatus> entity)
    {


        entity.ToTable("ActiveStatus", "Basic");

        entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedNever();
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.HasTimeToActiveAgain)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.IsActive)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.IsDeleted)
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();
        entity.Property(e => e.ModifyAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200);
    }
}