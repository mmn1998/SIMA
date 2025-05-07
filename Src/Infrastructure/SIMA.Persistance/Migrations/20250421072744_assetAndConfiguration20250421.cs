using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class assetAndConfiguration20250421 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LicenseStatus",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_LicenseStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "LicenseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseStatus_Code",
                schema: "AssetAndConfiguration",
                table: "LicenseStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItem_LicenseStatus_LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "LicenseStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "LicenseStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItem_LicenseStatus_LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropTable(
                name: "LicenseStatus",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationItem_LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "LicenseStatusId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");
        }
    }
}
