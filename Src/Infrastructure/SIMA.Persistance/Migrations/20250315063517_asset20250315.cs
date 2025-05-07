using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class asset20250315 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateSubject",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "BusinessCriticalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItem_BusinessCriticality_BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "BusinessCriticalityId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "BusinessCriticality",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItem_BusinessCriticality_BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationItem_BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "BusinessCriticalityId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "UpdateSubject",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");
        }
    }
}
