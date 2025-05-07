using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class goodsIdInOrderingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordering_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderResult_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "TenderResult");

            migrationBuilder.DropColumn(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropColumn(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropColumn(
                name: "DurationOfConsumption",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "DurationOfService",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "TenderResult",
                newName: "LogisticsSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_TenderResult_LogisticsRequestId",
                schema: "Logistics",
                table: "TenderResult",
                newName: "IX_TenderResult_LogisticsSupplyId");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "Ordering",
                newName: "LogisticsSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Ordering_LogisticsRequestId",
                schema: "Logistics",
                table: "Ordering",
                newName: "IX_Ordering_LogisticsSupplyId");

            migrationBuilder.AddColumn<long>(
                name: "CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ServiceDuration",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageDuration",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FinishedBy",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                schema: "IssueManagement",
                table: "Issue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsFinished",
                schema: "IssueManagement",
                table: "Issue",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoodsCategorySupplier",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    GoodsCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsCategorySupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsCategorySupplier_GoodsCategory_GoodsCategoryId",
                        column: x => x.GoodsCategoryId,
                        principalSchema: "Logistics",
                        principalTable: "GoodsCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GoodsCategorySupplier_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodsStatus",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsRequiredItConfirmation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogisticsSupply",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsSupply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsSupply_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderingItem",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderingId = table.Column<long>(type: "bigint", nullable: false),
                    GoodsId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderingItem_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalSchema: "Logistics",
                        principalTable: "Goods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderingItem_Ordering_OrderingId",
                        column: x => x.OrderingId,
                        principalSchema: "Logistics",
                        principalTable: "Ordering",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogisticsSupplyGoods",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestGoodsId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsSupplyGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsSupplyGoods_LogisticsRequestGoods_LogisticsRequestGoodsId",
                        column: x => x.LogisticsRequestGoodsId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsRequestGoods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LogisticsSupplyGoods_LogisticsSupply_LogisticsSupplyId",
                        column: x => x.LogisticsSupplyId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsSupply",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItem",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderingItemId = table.Column<long>(type: "bigint", nullable: false),
                    ReciptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItem_Documents_ReciptDocumentId",
                        column: x => x.ReciptDocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryItem_OrderingItem_OrderingItemId",
                        column: x => x.OrderingItemId,
                        principalSchema: "Logistics",
                        principalTable: "OrderingItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnOrderingItem",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OrderingItemId = table.Column<long>(type: "bigint", nullable: false),
                    ReciptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnOrderingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnOrderingItem_Documents_ReciptDocumentId",
                        column: x => x.ReciptDocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnOrderingItem_OrderingItem_OrderingItemId",
                        column: x => x.OrderingItemId,
                        principalSchema: "Logistics",
                        principalTable: "OrderingItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordering_CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering",
                column: "CandidatedSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestGoods_GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "GoodsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequestGoods_GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "GoodsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItem_OrderingItemId",
                schema: "Logistics",
                table: "DeliveryItem",
                column: "OrderingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItem_ReciptDocumentId",
                schema: "Logistics",
                table: "DeliveryItem",
                column: "ReciptDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategorySupplier_GoodsCategoryId",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                column: "GoodsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsCategorySupplier_SupplierId",
                schema: "Logistics",
                table: "GoodsCategorySupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsStatus_Code",
                schema: "Logistics",
                table: "GoodsStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupply_Code",
                schema: "Logistics",
                table: "LogisticsSupply",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupply_IssueId",
                schema: "Logistics",
                table: "LogisticsSupply",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupplyGoods_LogisticsRequestGoodsId",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                column: "LogisticsRequestGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupplyGoods_LogisticsSupplyId",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                column: "LogisticsSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderingItem_GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderingItem_OrderingId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "OrderingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrderingItem_OrderingItemId",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                column: "OrderingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrderingItem_ReciptDocumentId",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                column: "ReciptDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogisticsRequestGoods_GoodsCategory_GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "GoodsCategoryId",
                principalSchema: "Logistics",
                principalTable: "GoodsCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogisticsRequestGoods_GoodsStatus_GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                column: "GoodsStatusId",
                principalSchema: "Logistics",
                principalTable: "GoodsStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordering_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering",
                column: "CandidatedSupplierId",
                principalSchema: "Logistics",
                principalTable: "CandidatedSupplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordering_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "Ordering",
                column: "LogisticsSupplyId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupply",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderResult_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "TenderResult",
                column: "LogisticsSupplyId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupply",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogisticsRequestGoods_GoodsCategory_GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropForeignKey(
                name: "FK_LogisticsRequestGoods_GoodsStatus_GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordering_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordering_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderResult_LogisticsSupply_LogisticsSupplyId",
                schema: "Logistics",
                table: "TenderResult");

            migrationBuilder.DropTable(
                name: "DeliveryItem",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "GoodsCategorySupplier",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "GoodsStatus",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "LogisticsSupplyGoods",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "ReturnOrderingItem",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "LogisticsSupply",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "OrderingItem",
                schema: "Logistics");

            migrationBuilder.DropIndex(
                name: "IX_Ordering_CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropIndex(
                name: "IX_LogisticsRequestGoods_GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropIndex(
                name: "IX_LogisticsRequestGoods_GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "CandidatedSupplierId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "GoodsCategoryId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "GoodsStatusId",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "ServiceDuration",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "UsageDuration",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "FinishedBy",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "FinishedDate",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.RenameColumn(
                name: "LogisticsSupplyId",
                schema: "Logistics",
                table: "TenderResult",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_TenderResult_LogisticsSupplyId",
                schema: "Logistics",
                table: "TenderResult",
                newName: "IX_TenderResult_LogisticsRequestId");

            migrationBuilder.RenameColumn(
                name: "LogisticsSupplyId",
                schema: "Logistics",
                table: "Ordering",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Ordering_LogisticsSupplyId",
                schema: "Logistics",
                table: "Ordering",
                newName: "IX_Ordering_LogisticsRequestId");

            migrationBuilder.AddColumn<string>(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "Ordering",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "Ordering",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationOfConsumption",
                schema: "Logistics",
                table: "Goods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationOfService",
                schema: "Logistics",
                table: "Goods",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordering_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "Ordering",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderResult_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "TenderResult",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");
        }
    }
}
