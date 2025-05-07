using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.RenameColumn(
                name: "LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                newName: "LetterDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReferralLetter_LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                newName: "IX_ReferralLetter_LetterDocumentId");

            migrationBuilder.AddColumn<string>(
                name: "BranchLetterNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_LetterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "LetterDocumentId",
                principalSchema: "TrustyDraft",
                principalTable: "TrustyDraftDocument",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_LetterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.DropColumn(
                name: "BranchLetterNumber",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.RenameColumn(
                name: "LetterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                newName: "LeterDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReferralLetter_LetterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                newName: "IX_ReferralLetter_LeterDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralLetter_TrustyDraftDocument_LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "LeterDocumentId",
                principalSchema: "TrustyDraft",
                principalTable: "TrustyDraftDocument",
                principalColumn: "Id");
        }
    }
}
