using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class assetAndConfiguration20250422_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItem_TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "TimeMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationItem_TimeMeasurement_TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                column: "TimeMeasurementId",
                principalSchema: "Basic",
                principalTable: "TimeMeasurement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationItem_TimeMeasurement_TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationItem_TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "TimeMeasurementId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");
        }
    }
}
