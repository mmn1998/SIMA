using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class serviceCataloge20250203 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceType",
                schema: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_Code",
                schema: "ServiceCatalog",
                table: "ServiceType",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceType_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service",
                column: "ServiceTypeId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceType_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropTable(
                name: "ServiceType",
                schema: "ServiceCatalog");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                schema: "ServiceCatalog",
                table: "Service");
        }
    }
}
