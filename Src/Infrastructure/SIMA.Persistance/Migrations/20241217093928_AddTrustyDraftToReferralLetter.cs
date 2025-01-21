using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTrustyDraftToReferralLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferralLetter_TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "TrustyDraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralLetter_TrustyDraft_TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                column: "TrustyDraftId",
                principalSchema: "TrustyDraft",
                principalTable: "TrustyDraft",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralLetter_TrustyDraft_TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.DropIndex(
                name: "IX_ReferralLetter_TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter");

            migrationBuilder.DropColumn(
                name: "TrustyDraftId",
                schema: "TrustyDraft",
                table: "ReferralLetter");
        }
    }
}
