using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class chnageDataModelForState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_State",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_StateID",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "StateID",
                schema: "Project",
                table: "Step");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusID",
                schema: "Project",
                table: "Progress",
                newName: "ActiveStatusId");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                schema: "Project",
                table: "Progress",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IssueChangeHistoryConfiguration",
                schema: "IssueManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CurrentWorkflowId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStateId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    CurrenStepId = table.Column<long>(type: "bigint", nullable: false),
                    MainAggregateId = table.Column<long>(type: "bigint", nullable: true),
                    SourceId = table.Column<long>(type: "bigint", nullable: false),
                    IssueTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IssuePriorityId = table.Column<long>(type: "bigint", nullable: false),
                    IssueWeightCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueChangeHistoryConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_IssuePriority_IssuePriorityId",
                        column: x => x.IssuePriorityId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssuePriority",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_IssueType_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssueType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_IssueWeightCategory_IssueWeightCategoryId",
                        column: x => x.IssueWeightCategoryId,
                        principalSchema: "IssueManagement",
                        principalTable: "IssueWeightCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_MainAggregate_MainAggregateId",
                        column: x => x.MainAggregateId,
                        principalSchema: "Authentication",
                        principalTable: "MainAggregate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_State_CurrentStateId",
                        column: x => x.CurrentStateId,
                        principalSchema: "Project",
                        principalTable: "State",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_Step_CurrenStepId",
                        column: x => x.CurrenStepId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueChangeHistoryConfiguration_WorkFlow_CurrentWorkflowId",
                        column: x => x.CurrentWorkflowId,
                        principalSchema: "Project",
                        principalTable: "WorkFlow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progress_StateId",
                schema: "Project",
                table: "Progress",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_Code",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrenStepId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrentWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssuePriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueWeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueChangeHistoryConfiguration_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "MainAggregateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_State_StateId",
                schema: "Project",
                table: "Progress",
                column: "StateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_State_StateId",
                schema: "Project",
                table: "Progress");

            migrationBuilder.DropTable(
                name: "IssueChangeHistoryConfiguration",
                schema: "IssueManagement");

            migrationBuilder.DropIndex(
                name: "IX_Progress_StateId",
                schema: "Project",
                table: "Progress");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Project",
                table: "Progress");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusId",
                schema: "Project",
                table: "Progress",
                newName: "ActiveStatusID");

            migrationBuilder.AddColumn<long>(
                name: "StateID",
                schema: "Project",
                table: "Step",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Step_StateID",
                schema: "Project",
                table: "Step",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_State",
                schema: "Project",
                table: "Step",
                column: "StateID",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");
        }
    }
}
