using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationForGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationOfConsumption",
                schema: "Logistics",
                table: "Goods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationOfService",
                schema: "Logistics",
                table: "Goods",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationOfConsumption",
                schema: "Logistics",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "DurationOfService",
                schema: "Logistics",
                table: "Goods");
        }
    }
}
