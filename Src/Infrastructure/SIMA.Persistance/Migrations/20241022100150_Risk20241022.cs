using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Risk20241022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectedAsset_Risk",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "ARO",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "RiskManagement",
                table: "Threat",
                newName: "Description");

            migrationBuilder.AddColumn<long>(
                name: "ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "AV",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRiskImpact_ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact",
                column: "ServiceRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectedAsset_AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectedAsset_Asset_AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset",
                column: "AssetId",
                principalSchema: "AssetAndConfiguration",
                principalTable: "Asset",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectedAsset_Risk_RiskId",
                schema: "RiskManagement",
                table: "EffectedAsset",
                column: "RiskId",
                principalSchema: "RiskManagement",
                principalTable: "Risk",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRiskImpact_ServiceRisk_ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact",
                column: "ServiceRiskId",
                principalSchema: "ServiceCatalog",
                principalTable: "ServiceRisk",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectedAsset_Asset_AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectedAsset_Risk_RiskId",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRiskImpact_ServiceRisk_ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRiskImpact_ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact");

            migrationBuilder.DropIndex(
                name: "IX_EffectedAsset_AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.DropColumn(
                name: "ServiceRiskId",
                schema: "RiskManagement",
                table: "ServiceRiskImpact");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "RiskManagement",
                table: "EffectedAsset");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "RiskManagement",
                table: "Threat",
                newName: "Code");

            migrationBuilder.AlterColumn<double>(
                name: "AV",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ARO",
                schema: "RiskManagement",
                table: "EffectedAsset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectedAsset_Risk",
                schema: "RiskManagement",
                table: "EffectedAsset",
                column: "RiskId",
                principalSchema: "RiskManagement",
                principalTable: "Risk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
