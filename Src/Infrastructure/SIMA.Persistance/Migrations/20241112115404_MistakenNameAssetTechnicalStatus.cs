using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MistakenNameAssetTechnicalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_AssetTechnicalStatusConfiguration_AssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_FromAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_ToAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTechnicalStatusConfiguration",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatusConfiguration");

            migrationBuilder.RenameTable(
                name: "AssetTechnicalStatusConfiguration",
                schema: "AssetAndConfiguration",
                newName: "AssetTechnicalStatus",
                newSchema: "AssetAndConfiguration");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTechnicalStatusConfiguration_Code",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatus",
                newName: "IX_AssetTechnicalStatus_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTechnicalStatus",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AssetTechnicalStatus_AssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatus_FromAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "FromAssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatus_ToAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "ToAssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_AssetTechnicalStatus_AssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatus_FromAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatus_ToAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetTechnicalStatus",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatus");

            migrationBuilder.RenameTable(
                name: "AssetTechnicalStatus",
                schema: "AssetAndConfiguration",
                newName: "AssetTechnicalStatusConfiguration",
                newSchema: "AssetAndConfiguration");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTechnicalStatus_Code",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatusConfiguration",
                newName: "IX_AssetTechnicalStatusConfiguration_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetTechnicalStatusConfiguration",
                schema: "AssetAndConfiguration",
                table: "AssetTechnicalStatusConfiguration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AssetTechnicalStatusConfiguration_AssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "Asset",
                column: "AssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatusConfiguration",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_FromAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "FromAssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatusConfiguration",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetChangeTechnicalStatusHistory_AssetTechnicalStatusConfiguration_ToAssetTechnicalStatusId",
                schema: "AssetAndConfiguration",
                table: "AssetChangeTechnicalStatusHistory",
                column: "ToAssetTechnicalStatusId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "AssetTechnicalStatusConfiguration",
                principalColumn: "Id");
        }
    }
}
