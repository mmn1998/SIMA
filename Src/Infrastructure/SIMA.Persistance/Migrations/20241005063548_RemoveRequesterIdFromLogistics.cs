using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequesterIdFromLogistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogisticsRequest_Users_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.DropIndex(
                name: "IX_LogisticsRequest_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                schema: "IssueManagement",
                table: "Issue",
                newName: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_RequesterId",
                schema: "IssueManagement",
                table: "Issue",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Users_RequesterId",
                schema: "IssueManagement",
                table: "Issue",
                column: "RequesterId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Users_RequesterId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_RequesterId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.RenameColumn(
                name: "RequesterId",
                schema: "IssueManagement",
                table: "Issue",
                newName: "OwnerUserId");

            migrationBuilder.AddColumn<long>(
                name: "RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsRequest_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogisticsRequest_Users_RequesterId",
                schema: "Logistics",
                table: "LogisticsRequest",
                column: "RequesterId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
