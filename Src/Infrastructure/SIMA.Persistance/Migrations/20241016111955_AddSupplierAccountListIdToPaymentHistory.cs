using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierAccountListIdToPaymentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "SupplierAccountListId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentHistory_SupplierAccountList_SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory",
                column: "SupplierAccountListId",
                principalSchema: "Basic",
                principalTable: "SupplierAccountList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentHistory_SupplierAccountList_SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropIndex(
                name: "IX_PaymentHistory_SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory");

            migrationBuilder.DropColumn(
                name: "SupplierAccountListId",
                schema: "Logistics",
                table: "PaymentHistory");
        }
    }
}
