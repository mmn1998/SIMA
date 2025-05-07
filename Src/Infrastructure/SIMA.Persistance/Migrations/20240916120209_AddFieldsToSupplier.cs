using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IbanNo",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterNumber",
                schema: "Logistics",
                table: "Supplier",
                type: "nvarchar(26)",
                maxLength: 26,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IbanNo",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                schema: "Logistics",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "RegisterNumber",
                schema: "Logistics",
                table: "Supplier");
        }
    }
}
