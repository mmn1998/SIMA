using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20250105 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPaymentChannel_LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyPaymentChannel_Location_LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel",
                column: "LocationId",
                principalSchema: "Basic",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyPaymentChannel_Location_LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyPaymentChannel_LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "TrustyDraft",
                table: "CurrencyPaymentChannel");
        }
    }
}
