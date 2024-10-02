using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class LogisticsChanges14030701 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierType",
                schema: "Logistics",
                table: "Supplier",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SignatureId",
                schema: "Organization",
                table: "Staff",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EachPrice",
                schema: "Logistics",
                table: "LogisticsRequestGoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "OwnerUserId",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffSignatureId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupplierDocument",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierDocument_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Logistics",
                        principalTable: "Supplier",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StaffSignatureId",
                schema: "DMS",
                table: "Documents",
                column: "StaffSignatureId",
                unique: true,
                filter: "[StaffSignatureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDocument_DocumentId",
                schema: "Logistics",
                table: "SupplierDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDocument_SupplierId",
                schema: "Logistics",
                table: "SupplierDocument",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Staff_StaffSignatureId",
                schema: "DMS",
                table: "Documents",
                column: "StaffSignatureId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Staff_StaffSignatureId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "SupplierDocument",
                schema: "Logistics");

            migrationBuilder.DropIndex(
                name: "IX_Documents_StaffSignatureId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SupplierType",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SignatureId",
                schema: "Organization",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "EachPrice",
                schema: "Logistics",
                table: "LogisticsRequestGoods");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "StaffSignatureId",
                schema: "DMS",
                table: "Documents");
        }
    }
}
