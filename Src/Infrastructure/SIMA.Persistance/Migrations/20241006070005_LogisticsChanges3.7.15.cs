using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class LogisticsChanges3715 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFixedAsset",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                schema: "Basic",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                schema: "Basic",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsFixedAsset",
                schema: "Logistics",
                table: "GoodsCategory",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCode",
                schema: "Basic",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "NationalId",
                schema: "Basic",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "IsFixedAsset",
                schema: "Logistics",
                table: "GoodsCategory");

            migrationBuilder.AddColumn<string>(
                name: "IsFixedAsset",
                schema: "Logistics",
                table: "Goods",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
