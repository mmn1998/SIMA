using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateDatamodel20241217 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_WageRate_DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "DraftOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "PaymentTypeId",
                principalSchema: "Bank",
                principalTable: "PaymentType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WageRate_DraftOrigin_DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate",
                column: "DraftOriginId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftOrigin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_PaymentType_PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropForeignKey(
                name: "FK_WageRate_DraftOrigin_DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropIndex(
                name: "IX_WageRate_DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "WageRate");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.AlterColumn<long>(
                name: "LeterDocumentId",
                schema: "TrustyDraft",
                table: "ReferralLetter",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
