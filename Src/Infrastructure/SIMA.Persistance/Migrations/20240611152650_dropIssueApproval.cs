using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class dropIssueApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueApproval",
                schema: "IssueManagement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueApproval",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowActorId = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowStepId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    StepApprovalOptionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueApproval_Step_WorkflowStepId",
                        column: x => x.WorkflowStepId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueApproval_WorkFlowActor_WorkflowActorId",
                        column: x => x.WorkflowActorId,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueApproval_WorkflowActorId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "WorkflowActorId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueApproval_WorkflowStepId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "WorkflowStepId");
        }
    }
}
