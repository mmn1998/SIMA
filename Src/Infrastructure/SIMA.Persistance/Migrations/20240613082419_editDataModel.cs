using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class editDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "LogisticsRequestId",
                schema: "Logistics",
                table: "Goods",
                newName: "GoodsCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Goods_LogisticsRequestId",
                schema: "Logistics",
                table: "Goods",
                newName: "IX_Goods_GoodsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_GoodsCategory_GoodsCategoryId",
                schema: "Logistics",
                table: "Goods",
                column: "GoodsCategoryId",
                principalSchema: "Logistics",
                principalTable: "GoodsCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_GoodsCategory_GoodsCategoryId",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "GoodsCategoryId",
                schema: "Logistics",
                table: "Goods",
                newName: "LogisticsRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Goods_GoodsCategoryId",
                schema: "Logistics",
                table: "Goods",
                newName: "IX_Goods_LogisticsRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_LogisticsRequest_LogisticsRequestId",
                schema: "Logistics",
                table: "Goods",
                column: "LogisticsRequestId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequest",
                principalColumn: "Id");
        }
    }
}
