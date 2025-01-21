using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateModelState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_State_StateId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_StateId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Project",
                table: "Step");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StateId",
                schema: "Project",
                table: "Step",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Step_StateId",
                schema: "Project",
                table: "Step",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_State_StateId",
                schema: "Project",
                table: "Step",
                column: "StateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");
        }
    }
}
