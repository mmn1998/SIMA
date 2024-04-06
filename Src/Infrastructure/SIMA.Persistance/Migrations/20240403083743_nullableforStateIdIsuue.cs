using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class nullableforStateIdIsuue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_State_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_State_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.AlterColumn<long>(
                name: "SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CurrentStateId",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_State_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrentStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_State_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "SourceStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_State_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_State_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.AlterColumn<long>(
                name: "SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrentStateId",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_State_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrentStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_State_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "SourceStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
