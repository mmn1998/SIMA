using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateIssueApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueApproval",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    StepApprovalOptionId = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedBy = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowActorId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueApproval_Issue",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueApproval_StepApprovalOption",
                        column: x => x.StepApprovalOptionId,
                        principalSchema: "Project",
                        principalTable: "StepApprovalOption",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueApproval_WorkflowActor",
                        column: x => x.WorkflowActorId,
                        principalSchema: "Project",
                        principalTable: "WorkFlowActor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueApproval_IssueId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueApproval_StepApprovalOptionId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "StepApprovalOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueApproval_WorkflowActorId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "WorkflowActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueApproval",
                schema: "IssueManagement");
        }
    }
}
