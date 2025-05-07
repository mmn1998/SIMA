using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddApiMethodActionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApiMethodAction",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethodAction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedureParam_ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "ApiMethodActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodAction_Code",
                schema: "Basic",
                table: "ApiMethodAction",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressStoreProcedureParam_ApiMethodAction_ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "ApiMethodActionId",
                principalSchema: "Basic",
                principalTable: "ApiMethodAction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressStoreProcedureParam_ApiMethodAction_ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropTable(
                name: "ApiMethodAction",
                schema: "Basic");

            migrationBuilder.DropIndex(
                name: "IX_ProgressStoreProcedureParam_ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");

            migrationBuilder.DropColumn(
                name: "ApiMethodActionId",
                schema: "Project",
                table: "ProgressStoreProcedureParam");
        }
    }
}
