using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class asset20251003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAsset_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAssetHistory_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemDocument_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemIssue_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemIssue_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                newName: "IX_ConfigurationItemIssue_ConfigurationItemId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemDocument_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                newName: "IX_ConfigurationItemDocument_ConfigurationItemId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAssetHistory_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                newName: "IX_ConfigurationItemAssetHistory_ConfigurationItemId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                newName: "ConfigurationItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAsset_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                newName: "IX_ConfigurationItemAsset_ConfigurationItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAsset_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAssetHistory_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemDocument_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemIssue_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                column: "ConfigurationItemId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAsset_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemAssetHistory_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemDocument_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItemIssue_ConfigurationItem_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemIssue_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                newName: "IX_ConfigurationItemIssue_ConfigurationItemVersioningId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemDocument_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                newName: "IX_ConfigurationItemDocument_ConfigurationItemVersioningId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAssetHistory_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                newName: "IX_ConfigurationItemAssetHistory_ConfigurationItemVersioningId");

            migrationBuilder.RenameColumn(
                name: "ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                newName: "ConfigurationItemVersioningId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurationItemAsset_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                newName: "IX_ConfigurationItemAsset_ConfigurationItemVersioningId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAsset_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAsset",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemAssetHistory_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemAssetHistory",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemDocument_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemDocument",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItemIssue_ConfigurationItemVersioning_ConfigurationItemVersioningId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemIssue",
                column: "ConfigurationItemVersioningId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "ConfigurationItemVersioning",
                principalColumn: "Id");
        }
    }
}
