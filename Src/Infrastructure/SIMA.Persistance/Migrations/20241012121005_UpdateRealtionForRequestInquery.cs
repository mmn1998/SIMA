using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRealtionForRequestInquery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInquiry_LogisticsRequestDocument_InvoiceDocumentId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInquiry_LogisticsSupplyDocument_InvoiceDocumentId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "InvoiceDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsSupplyDocument",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInquiry_LogisticsSupplyDocument_InvoiceDocumentId",
                schema: "Logistics",
                table: "RequestInquiry");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInquiry_LogisticsRequestDocument_InvoiceDocumentId",
                schema: "Logistics",
                table: "RequestInquiry",
                column: "InvoiceDocumentId",
                principalSchema: "Logistics",
                principalTable: "LogisticsRequestDocument",
                principalColumn: "Id");
        }
    }
}
