using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeNameForReturnOrdering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryQuantity",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                newName: "ReturnQuantity");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                newName: "ReturnDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnQuantity",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                newName: "DeliveryQuantity");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                schema: "Logistics",
                table: "ReturnOrderingItem",
                newName: "DeliveryDate");
        }
    }
}
