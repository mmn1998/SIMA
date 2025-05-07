using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class removegoodsidfromorderingitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderingItem_Goods_GoodsId",
                schema: "Logistics",
                table: "OrderingItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderingItem_GoodsId",
                schema: "Logistics",
                table: "OrderingItem");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                schema: "Logistics",
                table: "OrderingItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderingItem_GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderingItem_Goods_GoodsId",
                schema: "Logistics",
                table: "OrderingItem",
                column: "GoodsId",
                principalSchema: "Logistics",
                principalTable: "Goods",
                principalColumn: "Id");
        }
    }
}
