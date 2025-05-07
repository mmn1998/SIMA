using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidatedSupplierToRequestInquery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestInquiry_CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "CandidatedSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInquiry_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "CandidatedSupplierId",
                principalSchema: "Logistics",
                principalTable: "CandidatedSupplier",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInquiry_CandidatedSupplier_CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropIndex(
                name: "IX_RequestInquiry_CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.DropColumn(
                name: "CandidatedSupplierId",
                schema: "Logistics",
                table: "RequestInquiry");
        }
    }
}
