using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class actorEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsActorManager",
                schema: "Project",
                table: "WorkFlowActor",
                type: "char(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsAssigneeForced",
                schema: "Project",
                table: "Step",
                type: "char(1)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkFlowActorEmployee",
                columns: table => new
                {
                    ActorId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowActorEmployee", x => new { x.EmployeeId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_WorkFlowActorEmployee_WorkFlowActor_ActorId",
                        column: x => x.ActorId,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorEmployee_ActorId",
                table: "WorkFlowActorEmployee",
                column: "ActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkFlowActorEmployee");

            migrationBuilder.DropColumn(
                name: "IsActorManager",
                schema: "Project",
                table: "WorkFlowActor");

            migrationBuilder.DropColumn(
                name: "IsAssigneeForced",
                schema: "Project",
                table: "Step");
        }
    }
}
