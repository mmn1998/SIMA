using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.BusinessEntities.Entities;
using SIMA.Domain.Models.Features.Auths.BusinessEntities.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths
{
   public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.ToTable("BusinessEntity", "Basic");
            entity.Property(x => x.Id)
                .HasConversion(
                 v => v.Value,
                 v => new BusinessEntityId(v)).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Name).HasMaxLength(50);

            
        }
    }
}
