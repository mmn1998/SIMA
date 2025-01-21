using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdatGoodsCategoy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Logistics.GoodsCategory.IX_GoodsCategory_Code1",
                schema: "Logistics",
                table: "GoodsCategory",
                newName: "IX_GoodsCategory_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_GoodsCategory_Code",
                schema: "Logistics",
                table: "GoodsCategory",
                newName: "Logistics.GoodsCategory.IX_GoodsCategory_Code1");
        }
    }
}
