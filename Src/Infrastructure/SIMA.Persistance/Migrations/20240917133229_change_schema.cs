using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class change_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorEmployee_WorkFlowActor_ActorId",
                table: "WorkFlowActorEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlowActorEmployee",
                table: "WorkFlowActorEmployee");

            migrationBuilder.RenameTable(
                name: "WorkFlowActorEmployee",
                newName: "WorkFlowActorEmployees",
                newSchema: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorEmployee_ActorId",
                schema: "Project",
                table: "WorkFlowActorEmployees",
                newName: "IX_WorkFlowActorEmployees_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlowActorEmployees",
                schema: "Project",
                table: "WorkFlowActorEmployees",
                columns: new[] { "EmployeeId", "ActorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorEmployees_WorkFlowActor_ActorId",
                schema: "Project",
                table: "WorkFlowActorEmployees",
                column: "ActorId",
                principalSchema: "Project",
                principalTable: "WorkFlowActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorEmployees_WorkFlowActor_ActorId",
                schema: "Project",
                table: "WorkFlowActorEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFlowActorEmployees",
                schema: "Project",
                table: "WorkFlowActorEmployees");

            migrationBuilder.RenameTable(
                name: "WorkFlowActorEmployees",
                schema: "Project",
                newName: "WorkFlowActorEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorEmployees_ActorId",
                table: "WorkFlowActorEmployee",
                newName: "IX_WorkFlowActorEmployee_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFlowActorEmployee",
                table: "WorkFlowActorEmployee",
                columns: new[] { "EmployeeId", "ActorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorEmployee_WorkFlowActor_ActorId",
                table: "WorkFlowActorEmployee",
                column: "ActorId",
                principalSchema: "Project",
                principalTable: "WorkFlowActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
