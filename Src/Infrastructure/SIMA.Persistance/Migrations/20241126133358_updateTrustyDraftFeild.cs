using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateTrustyDraftFeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "Bank",
                table: "Customer");

            migrationBuilder.AddColumn<decimal>(
                name: "DraftIssueWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                schema: "Bank",
                table: "Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "Bank",
                table: "Customer",
                column: "CustomerNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "Bank",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DraftIssueWage",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                schema: "Bank",
                table: "Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "Bank",
                table: "Customer",
                column: "CustomerNumber",
                unique: true,
                filter: "[CustomerNumber] IS NOT NULL");
        }
    }
}
