using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class assetAndConfiguration20250422 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Mtbf",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Mttr",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Uptime",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mtbf",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "Mttr",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "Uptime",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItem");
        }
    }
}
