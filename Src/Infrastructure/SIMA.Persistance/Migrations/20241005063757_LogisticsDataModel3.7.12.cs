using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class LogisticsDataModel3712 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropColumn(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryQuantity",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrePaymentPercentage",
                schema: "Logistics",
                table: "LogisticsSupplyGoods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryQuantity",
                schema: "Logistics",
                table: "DeliveryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryQuantity",
                schema: "Logistics",
                table: "ReturnOrderingItem");

            migrationBuilder.DropColumn(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "LogisticsSupplyGoods");

            migrationBuilder.DropColumn(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "LogisticsSupplyGoods");

            migrationBuilder.DropColumn(
                name: "PrePaymentPercentage",
                schema: "Logistics",
                table: "LogisticsSupplyGoods");

            migrationBuilder.DropColumn(
                name: "DeliveryQuantity",
                schema: "Logistics",
                table: "DeliveryItem");

            migrationBuilder.AddColumn<string>(
                name: "IsContractRequired",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsPrePaymentRequired",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: true);
        }
    }
}
