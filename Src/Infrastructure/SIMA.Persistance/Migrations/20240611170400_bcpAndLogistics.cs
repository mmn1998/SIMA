using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class bcpAndLogistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_Consequence_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_MaximumAcceptableOutage_MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanGeneralAssumption",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "MaximumAcceptableOutage",
                schema: "BCP");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.EnsureSchema(
                name: "Logistics");

            migrationBuilder.AddColumn<long>(
                name: "CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanPossibleAction",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanPossibleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanPossibleAction_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsQuorumPrice",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IsRequiredCeoConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsRequiredBoardConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsRequiredSupplierWrittenInquiry = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    MinPrice = table.Column<float>(type: "real", nullable: false),
                    MaxPrice = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsQuorumPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsType",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsRequireItConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HappeningPossibility",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ordering = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappeningPossibility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogisticsRequest",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsRequest_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecoveryPointObjective",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RpoFrom = table.Column<int>(type: "int", nullable: false),
                    RpoTo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecoveryPointObjective", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierRank",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Ordering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequireItConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierRank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasurement",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsRequireItConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasurement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsCategory",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GoodsTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsTechnological = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsHardware = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsGoods = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsRequiredSecurityCheck = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsCategory_GoodsType_GoodsTypeId",
                        column: x => x.GoodsTypeId,
                        principalSchema: "Logistics",
                        principalTable: "GoodsType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogisticsRequestDocument",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsRequestDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsRequestDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LogisticsRequestDocument_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentCommand",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    CommandDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommandDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrePayment = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCommand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCommand_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceEstimation",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimationPrice = table.Column<double>(type: "float", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceEstimation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceEstimation_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessImpactAnalysisDisasterOrigin",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessImpactAnalysisId = table.Column<long>(type: "bigint", nullable: false),
                    HappeningPossibilityId = table.Column<long>(type: "bigint", nullable: false),
                    ConsequenceId = table.Column<long>(type: "bigint", nullable: false),
                    RecoveryPointObjectiveId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RTO = table.Column<float>(type: "real", nullable: true),
                    RPO = table.Column<float>(type: "real", nullable: true),
                    WRT = table.Column<float>(type: "real", nullable: true),
                    MTD = table.Column<float>(type: "real", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessImpactAnalysisDisasterOrigin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDisasterOrigin_BusinessImpactAnalysis_BusinessImpactAnalysisId",
                        column: x => x.BusinessImpactAnalysisId,
                        principalSchema: "BCP",
                        principalTable: "BusinessImpactAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDisasterOrigin_Consequence_ConsequenceId",
                        column: x => x.ConsequenceId,
                        principalSchema: "BCP",
                        principalTable: "Consequence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDisasterOrigin_HappeningPossibility_HappeningPossibilityId",
                        column: x => x.HappeningPossibilityId,
                        principalSchema: "BCP",
                        principalTable: "HappeningPossibility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjective_RecoveryPointObjectiveId",
                        column: x => x.RecoveryPointObjectiveId,
                        principalSchema: "BCP",
                        principalTable: "RecoveryPointObjective",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierRankId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInBlackList = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_SupplierRank_SupplierRankId",
                        column: x => x.SupplierRankId,
                        principalSchema: "Logistics",
                        principalTable: "SupplierRank",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    UnitMeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    IsFixedAsset = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goods_UnitMeasurement_UnitMeasurementId",
                        column: x => x.UnitMeasurementId,
                        principalSchema: "Logistics",
                        principalTable: "UnitMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOrder",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_LogisticsRequestDocument_ReceiptDocumentId",
                        column: x => x.ReceiptDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ordering",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsContractRequired = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsPrePaymentRequired = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordering", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordering_LogisticsRequestDocument_ReceiptDocumentId",
                        column: x => x.ReceiptDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ordering_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiveOrder",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiveOrder_LogisticsRequestDocument_ReceiptDocumentId",
                        column: x => x.ReceiptDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveOrder_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestInquiry",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceDocumentId = table.Column<long>(type: "bigint", nullable: true),
                    InquieredPrice = table.Column<double>(type: "float", nullable: false),
                    IsWrittenInquiry = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestInquiry_LogisticsRequestDocument_InvoiceDocumentId",
                        column: x => x.InvoiceDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestInquiry_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnOrder",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnOrder_LogisticsRequestDocument_ReceiptDocumentId",
                        column: x => x.ReceiptDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnOrder_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierContract",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ContractDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContract_LogisticsRequestDocument_ContractDocumentId",
                        column: x => x.ContractDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierContract_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TenderResult",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    TenderDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderResult_LogisticsRequestDocument_TenderDocumentId",
                        column: x => x.TenderDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TenderResult_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentCommandId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrePayment = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_LogisticsRequestDocument_PaymentDocumentId",
                        column: x => x.PaymentDocumentId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestDocument",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentHistory_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentHistory_PaymentCommand_PaymentCommandId",
                        column: x => x.PaymentCommandId,
                        principalSchema: "Logistics",
                        principalTable: "PaymentCommand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalSchema: "Bank",
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidatedSupplier",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    IsSelected = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SelectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatedSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatedSupplier_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CandidatedSupplier_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierBlackListHistory",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierBlackListHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierBlackListHistory_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierBlackListHistory_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodsCoding",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    GoodsId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsCoding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsCoding_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalSchema: "Logistics",
                        principalTable: "Goods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GoodsCoding_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogisticsRequestGoods",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    GoodsId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsRequestGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsRequestGoods_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalSchema: "Logistics",
                        principalTable: "Goods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LogisticsRequestGoods_LogisticsRequest_LogisticsRequestId",
                        column: x => x.LogisticsRequestId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisCriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity",
                column: "CriticalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyService_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityStrategyRisk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanService_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanRisk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanPossibleAction_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanPossibleAction",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_BusinessImpactAnalysisId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "BusinessImpactAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_HappeningPossibilityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "HappeningPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysisDisasterOrigin_RecoveryPointObjectiveId",
                schema: "BCP",
                table: "BusinessImpactAnalysisDisasterOrigin",
                column: "RecoveryPointObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatedSupplier_LogisticsRequestId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatedSupplier_SupplierId",
                schema: "Logistics",
                table: "CandidatedSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_LogisticsRequestId",
                schema: "Logistics",
                table: "DeliveryOrder",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_ReceiptDocumentId",
                schema: "Logistics",
                table: "DeliveryOrder",
                column: "ReceiptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Code",
                schema: "Logistics",
                table: "Goods",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_LogisticsRequestId",
                schema: "Logistics",
                table: "Goods",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_UnitMeasurementId",
                schema: "Logistics",
                table: "Goods",
                column: "UnitMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategory_GoodsTypeId",
                schema: "Logistics",
                table: "GoodsCategory",
                column: "GoodsTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCoding_Code",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCoding_GoodsId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCoding_LogisticsRequestId",
                schema: "Logistics",
                table: "GoodsCoding",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsQuorumPrice_Code",
                schema: "Logistics",
                table: "GoodsQuorumPrice",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsType_Code",
                schema: "Logistics",
                table: "GoodsType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HappeningPossibility_Code",
                schema: "BCP",
                table: "HappeningPossibility",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequest_Code",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequest_IssueId",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestDocument_DocumentId",
                schema: "Logistics",
                table: "LogisticsRequestDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestDocument_LogisticsRequestId",
                schema: "Logistics",
                table: "LogisticsRequestDocument",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestGoods_GoodsId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestGoods_LogisticsRequestId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordering_LogisticsRequestId",
                schema: "Logistics",
                table: "Ordering",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordering_ReceiptDocumentId",
                schema: "Logistics",
                table: "Ordering",
                column: "ReceiptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCommand_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentCommand",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_LogisticsRequestId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_PaymentCommandId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "PaymentCommandId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_PaymentDocumentId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "PaymentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_PaymentTypeId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEstimation_LogisticsRequestId",
                schema: "Logistics",
                table: "PriceEstimation",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveOrder_LogisticsRequestId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveOrder_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "ReceiptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryPointObjective_Code",
                schema: "BCP",
                table: "RecoveryPointObjective",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInquiry_InvoiceDocumentId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "InvoiceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInquiry_LogisticsRequestId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrder_LogisticsRequestId",
                schema: "Logistics",
                table: "ReturnOrder",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrder_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReturnOrder",
                column: "ReceiptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Code",
                schema: "Logistics",
                table: "Supplier",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_SupplierRankId",
                schema: "Logistics",
                table: "Supplier",
                column: "SupplierRankId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBlackListHistory_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierBlackListHistory",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBlackListHistory_SupplierId",
                schema: "Logistics",
                table: "SupplierBlackListHistory",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContract_ContractDocumentId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "ContractDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContract_LogisticsRequestId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierRank_Code",
                schema: "Logistics",
                table: "SupplierRank",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TenderResult_LogisticsRequestId",
                schema: "Logistics",
                table: "TenderResult",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderResult_TenderDocumentId",
                schema: "Logistics",
                table: "TenderResult",
                column: "TenderDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasurement_Code",
                schema: "Logistics",
                table: "UnitMeasurement",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanRisk_Risk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk",
                column: "RiskId",
                principalSchema: "RiskManagement",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityPlanService_Service_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService",
                column: "ServiceId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStrategyRisk_Risk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk",
                column: "RiskId",
                principalSchema: "RiskManagement",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessContinuityStrategyService_Service_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService",
                column: "ServiceId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_Service_ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ServiceId",
                principalSchema: "ServiceCatalog",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysisCriticalActivity_CriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity",
                column: "CriticalActivityId",
                principalSchema: "ServiceCatalog",
                principalTable: "CriticalActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanRisk_Risk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityPlanService_Service_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStrategyRisk_Risk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessContinuityStrategyService_Service_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysis_Service_ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessImpactAnalysisCriticalActivity_CriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity");

            migrationBuilder.DropTable(
                name: "BusinessContinuityPlanPossibleAction",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "BusinessImpactAnalysisDisasterOrigin",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "CandidatedSupplier",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "DeliveryOrder",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "GoodsCategory",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "GoodsCoding",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "GoodsQuorumPrice",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "LogisticsRequestGoods",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "Ordering",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "PaymentHistory",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "PriceEstimation",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "ReceiveOrder",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "RequestInquiry",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "ReturnOrder",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "SupplierBlackListHistory",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "SupplierContract",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "TenderResult",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "HappeningPossibility",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "RecoveryPointObjective",
                schema: "BCP");

            migrationBuilder.DropTable(
                name: "GoodsType",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "Goods",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "PaymentCommand",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "LogisticsRequestDocument",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "UnitMeasurement",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "SupplierRank",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "LogisticsRequest",
                schema: "Logistics");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysisCriticalActivity_CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity");

            migrationBuilder.DropIndex(
                name: "IX_BusinessImpactAnalysis_ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategyService_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityStrategyRisk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanService_ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropIndex(
                name: "IX_BusinessContinuityPlanRisk_RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.DropColumn(
                name: "CriticalActivityId",
                schema: "BCP",
                table: "BusinessImpactAnalysisCriticalActivity");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessContinuityStrategyService");

            migrationBuilder.DropColumn(
                name: "RiskId",
                schema: "BCP",
                table: "BusinessContinuityStrategyRisk");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "BCP",
                table: "BusinessContinuityPlanService");

            migrationBuilder.DropColumn(
                name: "RiskId",
                schema: "BCP",
                table: "BusinessContinuityPlanRisk");

            migrationBuilder.AddColumn<long>(
                name: "ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessContinuityPlanGeneralAssumption",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BusinessContinuityPlanId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContinuityPlanGeneralAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContinuityPlanGeneralAssumption_BusinessContinuityPlan_BusinessContinuityPlanId",
                        column: x => x.BusinessContinuityPlanId,
                        principalSchema: "BCP",
                        principalTable: "BusinessContinuityPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaximumAcceptableOutage",
                schema: "BCP",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DurationHourFrom = table.Column<int>(type: "int", nullable: false),
                    DurationHourTo = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaximumAcceptableOutage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ConsequenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessImpactAnalysis_MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "MaximumAcceptableOutageId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanGeneralAssumption_BusinessContinuityPlanId",
                schema: "BCP",
                table: "BusinessContinuityPlanGeneralAssumption",
                column: "BusinessContinuityPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContinuityPlanGeneralAssumption_Code",
                schema: "BCP",
                table: "BusinessContinuityPlanGeneralAssumption",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MaximumAcceptableOutage_Code",
                schema: "BCP",
                table: "MaximumAcceptableOutage",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_Consequence_ConsequenceId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "ConsequenceId",
                principalSchema: "BCP",
                principalTable: "Consequence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessImpactAnalysis_MaximumAcceptableOutage_MaximumAcceptableOutageId",
                schema: "BCP",
                table: "BusinessImpactAnalysis",
                column: "MaximumAcceptableOutageId",
                principalSchema: "BCP",
                principalTable: "MaximumAcceptableOutage",
                principalColumn: "Id");
        }
    }
}
