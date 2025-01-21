using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241207 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.DropIndex(
                name: "IX_ReferralLetter_ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.DropColumn(
                name: "ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetter_ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "ReceiptDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_ReceiptDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "ReceiptDocumentId",
                principalSchema: "TrustyDraft",
                principalTable: "TrustyDraftDocument",
                principalColumn: "Id");
        }
    }
}
