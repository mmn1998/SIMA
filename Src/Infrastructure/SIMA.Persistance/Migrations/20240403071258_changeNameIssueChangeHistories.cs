using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class changeNameIssueChangeHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssuePriority_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssueType_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssueWeightCategory_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_State_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueChangeHistoryConfiguration",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration");

            migrationBuilder.RenameTable(
                name: "IssueChangeHistoryConfiguration",
                schema: "IssueManagement",
                newName: "IssueChangeHistory",
                newSchema: "IssueManagement");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_MainAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_IssueWeightCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_IssueTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_IssuePriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_CurrentWorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_CurrentStateId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_CurrenStepId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistoryConfiguration_Code",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                newName: "IX_IssueChangeHistory_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueChangeHistory",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_IssuePriority_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "IssuePriorityId",
                principalSchema: "IssueManagement",
                principalTable: "IssuePriority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_IssueType_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "IssueTypeId",
                principalSchema: "IssueManagement",
                principalTable: "IssueType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_IssueWeightCategory_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "IssueWeightCategoryId",
                principalSchema: "IssueManagement",
                principalTable: "IssueWeightCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "IssueId",
                principalSchema: "IssueManagement",
                principalTable: "Issue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "MainAggregateId",
                principalSchema: "Authentication",
                principalTable: "MainAggregate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_State_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "CurrentStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "CurrenStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "CurrentWorkflowId",
                principalSchema: "Project",
                principalTable: "WorkFlow",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_IssuePriority_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_IssueType_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_IssueWeightCategory_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_State_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueChangeHistory",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.RenameTable(
                name: "IssueChangeHistory",
                schema: "IssueManagement",
                newName: "IssueChangeHistoryConfiguration",
                newSchema: "IssueManagement");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_MainAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_IssueWeightCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_IssueTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_IssuePriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_CurrentWorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_CurrentStateId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_CurrenStepId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueChangeHistory_Code",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                newName: "IX_IssueChangeHistoryConfiguration_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueChangeHistoryConfiguration",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssuePriority_IssuePriorityId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssuePriorityId",
                principalSchema: "IssueManagement",
                principalTable: "IssuePriority",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssueType_IssueTypeId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueTypeId",
                principalSchema: "IssueManagement",
                principalTable: "IssueType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_IssueWeightCategory_IssueWeightCategoryId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueWeightCategoryId",
                principalSchema: "IssueManagement",
                principalTable: "IssueWeightCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "IssueId",
                principalSchema: "IssueManagement",
                principalTable: "Issue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "MainAggregateId",
                principalSchema: "Authentication",
                principalTable: "MainAggregate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_State_CurrentStateId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrentStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrenStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistoryConfiguration_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistoryConfiguration",
                column: "CurrentWorkflowId",
                principalSchema: "Project",
                principalTable: "WorkFlow",
                principalColumn: "Id");
        }
    }
}
