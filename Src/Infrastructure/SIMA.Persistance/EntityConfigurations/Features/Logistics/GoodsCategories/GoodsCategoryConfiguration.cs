using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequestGoodss.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.Entities;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;

namespace SIMA.Persistance.EntityConfigurations.Features.Logistics.GoodsCategories;

public class GoodsCategoryConfiguration : IEntityTypeConfiguration<GoodsCategory>
{
    public void Configure(EntityTypeBuilder<GoodsCategory> entity)
    {
        entity.ToTable("GoodsCategory", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsCategoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IsGoods).HasMaxLength(1);
        entity.Property(x => x.IsHardware).HasMaxLength(1);
        entity.Property(x => x.IsRequiredSecurityCheck).HasMaxLength(1);
        entity.Property(x => x.IsTechnological).HasMaxLength(1);

        entity.Property(x => x.GoodsTypeId)
            .HasConversion(x => x.Value, x => new GoodsTypeId(x));
        entity.HasOne(x => x.GoodsType)
            .WithMany(x => x.GoodsCategories)
            .HasForeignKey(x => x.GoodsTypeId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
{
    public void Configure(EntityTypeBuilder<Goods> entity)
    {
        entity.ToTable("Goods", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IsFixedAsset).HasMaxLength(1);

        entity.Property(x => x.UnitMeasurementId)
            .HasConversion(x => x.Value, x => new UnitMeasurementId(x));
        entity.HasOne(x => x.UnitMeasurement)
            .WithMany(x => x.Goods)
            .HasForeignKey(x => x.UnitMeasurementId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.GoodsCategoryId)
            .HasConversion(x => x.Value, x => new GoodsCategoryId(x));
        entity.HasOne(x => x.GoodsCategory)
            .WithMany(x => x.Goods)
            .HasForeignKey(x => x.GoodsCategoryId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> entity)
    {
        entity.ToTable("Supplier", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new SupplierId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IsInBlackList).HasMaxLength(1);

        entity.Property(x => x.SupplierRankId)
            .HasConversion(x => x.Value, x => new SupplierRankId(x));
        entity.HasOne(x => x.SupplierRank)
            .WithMany(x => x.Suppliers)
            .HasForeignKey(x => x.SupplierRankId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class UnitMeasurementConfiguration : IEntityTypeConfiguration<UnitMeasurement>
{
    public void Configure(EntityTypeBuilder<UnitMeasurement> entity)
    {
        entity.ToTable("UnitMeasurement", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new UnitMeasurementId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IsRequireItConfirmation).HasMaxLength(1);
    }
}
public class CandidatedSupplierConfiguration : IEntityTypeConfiguration<CandidatedSupplier>
{
    public void Configure(EntityTypeBuilder<CandidatedSupplier> entity)
    {
        entity.ToTable("CandidatedSupplier", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new CandidatedSupplierId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.IsSelected).HasMaxLength(1);

        entity.Property(x => x.SupplierId)
            .HasConversion(x => x.Value, x => new SupplierId(x));
        entity.HasOne(x => x.Supplier)
            .WithMany(x => x.CandidatedSuppliers)
            .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.CandidatedSuppliers)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class SupplierBlackListHistoryConfiguration : IEntityTypeConfiguration<SupplierBlackListHistory>
{
    public void Configure(EntityTypeBuilder<SupplierBlackListHistory> entity)
    {
        entity.ToTable("SupplierBlackListHistory", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new SupplierBlackListHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.SupplierId)
            .HasConversion(x => x.Value, x => new SupplierId(x));
        entity.HasOne(x => x.Supplier)
            .WithMany(x => x.SupplierBlackListHistories)
            .HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.SupplierBlackListHistories)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class SupplierRankConfiguration : IEntityTypeConfiguration<SupplierRank>
{
    public void Configure(EntityTypeBuilder<SupplierRank> entity)
    {
        entity.ToTable("SupplierRank", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new SupplierRankId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Name).HasMaxLength(200).IsUnicode();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(x => x.IsRequireItConfirmation).HasMaxLength(1);
    }
}
public class PaymentCommandConfiguration : IEntityTypeConfiguration<PaymentCommand>
{
    public void Configure(EntityTypeBuilder<PaymentCommand> entity)
    {
        entity.ToTable("PaymentCommand", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new PaymentCommandId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(x => x.IsPrePayment).HasMaxLength(1);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.PaymentCommands)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class PriceEstimationConfiguration : IEntityTypeConfiguration<PriceEstimation>
{
    public void Configure(EntityTypeBuilder<PriceEstimation> entity)
    {
        entity.ToTable("PriceEstimation", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new PriceEstimationId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.PriceEstimations)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class GoodsCodingConfiguration : IEntityTypeConfiguration<GoodsCoding>
{
    public void Configure(EntityTypeBuilder<GoodsCoding> entity)
    {
        entity.ToTable("GoodsCoding", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsCodingId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.GoodsId)
            .HasConversion(x => x.Value, x => new GoodsId(x));
        entity.HasOne(x => x.Goods)
            .WithMany(x => x.GoodsCodings)
            .HasForeignKey(x => x.GoodsId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.GoodsCodings)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
{
    public void Configure(EntityTypeBuilder<DeliveryOrder> entity)
    {
        entity.ToTable("DeliveryOrder", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new DeliveryOrderId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ReceiptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ReceiptDocument)
            .WithMany(x => x.DeliveryOrders)
            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.DeliveryOrders)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class LogisticsRequestDocumentConfiguration : IEntityTypeConfiguration<LogisticsRequestDocument>
{
    public void Configure(EntityTypeBuilder<LogisticsRequestDocument> entity)
    {
        entity.ToTable("LogisticsRequestDocument", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsRequestDocumentId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.DocumentId)
            .HasConversion(x => x.Value, x => new DocumentId(x));
        entity.HasOne(x => x.Document)
            .WithMany(x => x.LogisticsRequestDocuments)
            .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.LogisticsRequestDocuments)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class OrderingConfiguration : IEntityTypeConfiguration<Ordering>
{
    public void Configure(EntityTypeBuilder<Ordering> entity)
    {
        entity.ToTable("Ordering", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new OrderingId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.IsContractRequired).HasMaxLength(1);
        entity.Property(e => e.IsPrePaymentRequired).HasMaxLength(1);

        entity.Property(x => x.ReceiptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ReceiptDocument)
            .WithMany(x => x.Orderings)
            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.Orderings)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class ReceiveOrderConfiguration : IEntityTypeConfiguration<ReceiveOrder>
{
    public void Configure(EntityTypeBuilder<ReceiveOrder> entity)
    {
        entity.ToTable("ReceiveOrder", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ReceiveOrderId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ReceiptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ReceiptDocument)
            .WithMany(x => x.ReceiveOrders)
            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.ReceiveOrders)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class ReturnOrderConfiguration : IEntityTypeConfiguration<ReturnOrder>
{
    public void Configure(EntityTypeBuilder<ReturnOrder> entity)
    {
        entity.ToTable("ReturnOrder", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new ReturnOrderId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ReceiptDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ReceiptDocument)
            .WithMany(x => x.ReturnOrders)
            .HasForeignKey(x => x.ReceiptDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.ReturnOrders)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class RequestInquiryConfiguration : IEntityTypeConfiguration<RequestInquiry>
{
    public void Configure(EntityTypeBuilder<RequestInquiry> entity)
    {
        entity.ToTable("RequestInquiry", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new RequestInquiryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.IsWrittenInquiry).HasMaxLength(1);

        entity.Property(x => x.InvoiceDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.InvoiceDocument)
            .WithMany(x => x.RequestInquiries)
            .HasForeignKey(x => x.InvoiceDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.RequestInquiries)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class SupplierContractConfiguration : IEntityTypeConfiguration<SupplierContract>
{
    public void Configure(EntityTypeBuilder<SupplierContract> entity)
    {
        entity.ToTable("SupplierContract", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new SupplierContractId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.ContractDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.ContractDocument)
            .WithMany(x => x.SupplierContracts)
            .HasForeignKey(x => x.ContractDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.SupplierContracts)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class TenderResultConfiguration : IEntityTypeConfiguration<TenderResult>
{
    public void Configure(EntityTypeBuilder<TenderResult> entity)
    {
        entity.ToTable("TenderResult", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new TenderResultId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.TenderDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.TenderDocument)
            .WithMany(x => x.TenderResults)
            .HasForeignKey(x => x.TenderDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.TenderResults)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class PaymentHistoryConfiguration : IEntityTypeConfiguration<PaymentHistory>
{
    public void Configure(EntityTypeBuilder<PaymentHistory> entity)
    {
        entity.ToTable("PaymentHistory", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new PaymentHistoryId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.IsPrePayment).HasMaxLength(1);

        entity.Property(x => x.PaymentTypeId)
            .HasConversion(x => x.Value, x => new PaymentTypeId(x));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentTypeId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.PaymentCommandId)
            .HasConversion(x => x.Value, x => new PaymentCommandId(x));
        entity.HasOne(x => x.PaymentType)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentTypeId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.PaymentDocumentId)
            .HasConversion(x => x.Value, x => new LogisticsRequestDocumentId(x));
        entity.HasOne(x => x.PaymentDocument)
            .WithMany(x => x.PaymentHistories)
            .HasForeignKey(x => x.PaymentDocumentId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class LogisticsRequestGoodsConfiguration : IEntityTypeConfiguration<LogisticsRequestGoods>
{
    public void Configure(EntityTypeBuilder<LogisticsRequestGoods> entity)
    {
        entity.ToTable("LogisticsRequestGoods", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsRequestGoodsId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();

        entity.Property(x => x.GoodsId)
            .HasConversion(x => x.Value, x => new GoodsId(x));
        entity.HasOne(x => x.Goods)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.GoodsId).OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.Property(x => x.LogisticsRequestId)
            .HasConversion(x => x.Value, x => new LogisticsRequestId(x));
        entity.HasOne(x => x.LogisticsRequest)
            .WithMany(x => x.LogisticsRequestGoods)
            .HasForeignKey(x => x.LogisticsRequestId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class LogisticsRequestConfiguration : IEntityTypeConfiguration<LogisticsRequest>
{
    public void Configure(EntityTypeBuilder<LogisticsRequest> entity)
    {
        entity.ToTable("LogisticsRequest", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new LogisticsRequestId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();

        entity.Property(x => x.IssueId)
            .HasConversion(x => x.Value, x => new IssueId(x));
        entity.HasOne(x => x.Issue)
            .WithMany(x => x.Logistics)
            .HasForeignKey(x => x.IssueId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}
public class GoodsTypeConfiguration : IEntityTypeConfiguration<GoodsType>
{
    public void Configure(EntityTypeBuilder<GoodsType> entity)
    {
        entity.ToTable("GoodsType", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsTypeId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.IsRequireItConfirmation).HasMaxLength(1);
    }
}
public class GoodsQuorumPriceConfiguration : IEntityTypeConfiguration<GoodsQuorumPrice>
{
    public void Configure(EntityTypeBuilder<GoodsQuorumPrice> entity)
    {
        entity.ToTable("GoodsQuorumPrice", "Logistics");
        entity.Property(x => x.Id)
    .HasConversion(
        v => v.Value,
        v => new GoodsQuorumPriceId(v))
    .ValueGeneratedNever();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnType("datetime");
        entity.Property(e => e.ModifiedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken();
        entity.HasIndex(e => e.Code).IsUnique();
        entity.Property(e => e.Code)
                    .HasMaxLength(20).IsUnicode();
        entity.Property(e => e.Name)
                    .HasMaxLength(200).IsUnicode();
        entity.Property(e => e.IsRequiredBoardConfirmation).HasMaxLength(1);
        entity.Property(e => e.IsRequiredCeoConfirmation).HasMaxLength(1);
        entity.Property(e => e.IsRequiredSupplierWrittenInquiry).HasMaxLength(1);
    }
}
