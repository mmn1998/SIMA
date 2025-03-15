using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trusty20250223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryResponse_ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "ExcessWageCurrencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryResponse_CurrencyType_ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse",
                column: "ExcessWageCurrencyTypeId",
                principalSchema: "Bank",
                principalTable: "CurrencyType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryResponse_CurrencyType_ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            migrationBuilder.DropIndex(
                name: "IX_InquiryResponse_ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse");

            migrationBuilder.DropColumn(
                name: "ExcessWageCurrencyTypeId",
                schema: "TrustyDraft",
                table: "InquiryResponse");
        }
    }
}
