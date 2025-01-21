using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataModelForBasicAndAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "AssetAndConfiguration",
                table: "Asset",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessEntity",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplexAsset",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    ParentAssetId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplexAsset_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComplexAsset_Asset_ParentAssetId",
                        column: x => x.ParentAssetId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplexAsset_AssetId",
                schema: "AssetAndConfiguration",
                table: "ComplexAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexAsset_ParentAssetId",
                schema: "AssetAndConfiguration",
                table: "ComplexAsset",
                column: "ParentAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessEntity",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "ComplexAsset",
                schema: "AssetAndConfiguration");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "AssetAndConfiguration",
                table: "Asset");
        }
    }
}
