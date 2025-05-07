using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationSupplyDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItem_Documents_ReciptDocumentId",
                schema: "Logistics",
                table: "DeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordering_LogisticsRequestDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_LogisticsRequestDocument_PaymentDocumentId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveOrder_LogisticsRequestDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReceiveOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnOrderingItem_Documents_ReciptDocumentId",
                schema: "Logistics",
                table: "ReturnOrderingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContract_LogisticsRequestDocument_ContractDocumentId",
                schema: "Logistics",
                table: "SupplierContract");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderResult_LogisticsRequestDocument_TenderDocumentId",
                schema: "Logistics",
                table: "TenderResult");

            migrationBuilder.DropTable(
                name: "DeliveryOrder",
                schema: "Logistics");

            migrationBuilder.DropTable(
                name: "ReturnOrder",
                schema: "Logistics");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItem_LogisticsSupplyDocument_ReciptDocumentId",
                schema: "Logistics",
                table: "DeliveryItem",
                column: "ReciptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordering_LogisticsSupplyDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "Ordering",
                column: "ReceiptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_LogisticsSupplyDocument_PaymentDocumentId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "PaymentDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveOrder_LogisticsSupplyDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "ReceiptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnOrderingItem_LogisticsSupplyDocument_ReciptDocumentId",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                column: "ReciptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContract_LogisticsSupplyDocument_ContractDocumentId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "ContractDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderResult_LogisticsSupplyDocument_TenderDocumentId",
                schema: "Logistics",
                table: "TenderResult",
                column: "TenderDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItem_LogisticsSupplyDocument_ReciptDocumentId",
                schema: "Logistics",
                table: "DeliveryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordering_LogisticsSupplyDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "Ordering");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_LogisticsSupplyDocument_PaymentDocumentId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveOrder_LogisticsSupplyDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReceiveOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnOrderingItem_LogisticsSupplyDocument_ReciptDocumentId",
                schema: "Logistics",
                table: "ReturnOrderingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierContract_LogisticsSupplyDocument_ContractDocumentId",
                schema: "Logistics",
                table: "SupplierContract");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderResult_LogisticsSupplyDocument_TenderDocumentId",
                schema: "Logistics",
                table: "TenderResult");

            migrationBuilder.CreateTable(
                name: "DeliveryOrder",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ReturnOrder",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_ReturnOrder_LogisticsRequestId",
                schema: "Logistics",
                table: "ReturnOrder",
                column: "LogisticsRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnOrder_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReturnOrder",
                column: "ReceiptDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItem_Documents_ReciptDocumentId",
                schema: "Logistics",
                table: "DeliveryItem",
                column: "ReciptDocumentId",
                principalSchema: "DMS",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordering_LogisticsRequestDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "Ordering",
                column: "ReceiptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_LogisticsRequestDocument_PaymentDocumentId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "PaymentDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveOrder_LogisticsRequestDocument_ReceiptDocumentId",
                schema: "Logistics",
                table: "ReceiveOrder",
                column: "ReceiptDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnOrderingItem_Documents_ReciptDocumentId",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                column: "ReciptDocumentId",
                principalSchema: "DMS",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierContract_LogisticsRequestDocument_ContractDocumentId",
                schema: "Logistics",
                table: "SupplierContract",
                column: "ContractDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderResult_LogisticsRequestDocument_TenderDocumentId",
                schema: "Logistics",
                table: "TenderResult",
                column: "TenderDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");
        }
    }
}
