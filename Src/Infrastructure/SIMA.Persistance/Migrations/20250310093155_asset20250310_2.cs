using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class asset20250310_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCustomFieldOption",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetCustomFieldId = table.Column<long>(type: "bigint", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCustomFieldOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCustomFieldOption_AssetCustomField_AssetCustomFieldId",
                        column: x => x.AssetCustomFieldId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "AssetCustomField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItemCustomFieldOption",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemCustomFieldOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemCustomFieldOption_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetCustomFieldOption_AssetCustomFieldId",
                schema: "Asset",
                table: "AssetCustomFieldOption",
                column: "AssetCustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemCustomFieldOption_ConfigurationItemId",
                schema: "Asset",
                table: "ConfigurationItemCustomFieldOption",
                column: "ConfigurationItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetCustomFieldOption",
                schema: "Asset");

            migrationBuilder.DropTable(
                name: "ConfigurationItemCustomFieldOption",
                schema: "Asset");
        }
    }
}
