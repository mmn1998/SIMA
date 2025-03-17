//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;

//namespace SIMA.Persistance.EntityConfigurations.Features.BCP.BusinessContinuityPlans
//{
//    public class BusinessContinuityPlanVersioningConfiguration : IEntityTypeConfiguration<BusinessContinuityPlanVersioning>
//    {
//        public void Configure(EntityTypeBuilder<BusinessContinuityPlanVersioning> entity)
//        {
//            entity.ToTable("BusinessContinuityPlanVersioning", "BCP");
//            entity.Property(x => x.Id)
//                .HasConversion(
//                 v => v.Value,
//                 v => new(v)).ValueGeneratedNever();
//            entity.HasKey(i => i.Id);
//            entity.Property(e => e.VersionNumber).HasMaxLength(20).IsUnicode(true);
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime");

//            entity.Property(e => e.ModifiedAt)
//                .IsRowVersion()
//                .IsConcurrencyToken();

//            entity.Property(x => x.BusinessContinuityPlanId)
//                .HasConversion(
//                x => x.Value,
//                x => new BusinessContinuityPlanId(x)
//                );
//            entity.HasOne(x => x.BusinessContinuityPlan)
//                .WithMany(x => x.BusinessContinuityPlanVersionings)
//                .HasForeignKey(x => x.BusinessContinuityPlanId);
//        }
//    }
//}
