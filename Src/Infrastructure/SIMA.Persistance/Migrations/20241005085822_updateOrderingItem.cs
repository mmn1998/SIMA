using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderingItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ItemPrice",
                schema: "Logistics",
                table: "OrderingItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderingItem_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "LogisticsSupplyGoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderingItem_LogisticsSupplyGoods_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "LogisticsSupplyGoodsId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyGoods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderingItem_LogisticsSupplyGoods_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderingItem_LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                schema: "Logistics",
                table: "OrderingItem");

            migrationBuilder.DropColumn(
                name: "LogisticsSupplyGoodsId",
                schema: "Logistics",
                table: "OrderingItem");
        }
    }
}
