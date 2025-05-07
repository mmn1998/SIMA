using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Auths;

internal class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> entity)
    {
        entity.ToTable("Gender", "Basic");

        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.Id)
            .HasConversion(
             v => v.Value,
             v => new GenderId(v)).ValueGeneratedNever();
        entity.HasKey(i => i.Id);
        entity.Property(e => e.Code).HasMaxLength(50);
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
            .IsRowVersion()
            .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(50);

        #region SeedData
        //entity.HasData(new List<Gender>
        //{
        //    Gender.Create(new CreateGenderArg
        //    {
        //        Id = 1,
        //        ActiveStatusId = 1,
        //        Code = "01",
        //        Name = "مرد"
        //    }).GetAwaiter().GetResult(),
        //    Gender.Create(new CreateGenderArg
        //    {
        //        Id = 2,
        //        ActiveStatusId = 1,
        //        Code = "02",
        //        Name = "زن"
        //    }).GetAwaiter().GetResult()
        //});
        #endregion
    }
}
