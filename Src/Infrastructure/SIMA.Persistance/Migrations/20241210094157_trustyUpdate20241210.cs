using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241210 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "InquiryRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "WageDeductionMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_InquiryRequest_InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "InquiryRequestId",
                principalSchema: "TrustyDraft",
                principalTable: "InquiryRequest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_WageDeductionMethod_WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "WageDeductionMethodId",
                principalSchema: "TrustyDraft",
                principalTable: "WageDeductionMethod",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_InquiryRequest_InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_WageDeductionMethod_WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "InquiryRequestId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "WageDeductionMethodId",
                schema: "TrustyDraft",
                table: "TrustyDraft");
        }
    }
}
