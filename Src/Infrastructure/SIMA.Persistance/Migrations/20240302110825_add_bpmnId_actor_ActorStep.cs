using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class add_bpmnId_actor_ActorStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BpmnId",
                schema: "Project",
                table: "WorkFlowActorStep",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BpmnId",
                schema: "Project",
                table: "WorkFlowActor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BpmnId",
                schema: "Project",
                table: "WorkFlowActorStep");

            migrationBuilder.DropColumn(
                name: "BpmnId",
                schema: "Project",
                table: "WorkFlowActor");
        }
    }
}
