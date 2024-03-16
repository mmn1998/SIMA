using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addremainingfks20240220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGroup_Project_ProjectID",
                schema: "Project",
                table: "ProjectGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorGroup_WorkFlowActor_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorGroup");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorUser_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "IX_WorkFlowActorUser_WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorRole_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "IX_WorkFlowActorRole_WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "ActiveStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorGroup_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "IX_WorkFlowActorGroup_WorkFlowActorId");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusID",
                schema: "Project",
                table: "WorkFlowActor",
                newName: "ActiveStatusId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "Project",
                table: "ProjectGroup",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                schema: "Project",
                table: "ProjectGroup",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusID",
                schema: "Project",
                table: "ProjectGroup",
                newName: "ActiveStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectGroup_ProjectID",
                schema: "Project",
                table: "ProjectGroup",
                newName: "IX_ProjectGroup_ProjectId");

            migrationBuilder.AlterColumn<long>(
                name: "ManagerRoleID",
                schema: "Project",
                table: "WorkFlow",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "MainAggregateId",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SourceId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "MainAggregateId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AttachStepId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowCompany_CompanyId",
                schema: "Project",
                table: "WorkFlowCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorUser_UserId",
                schema: "Project",
                table: "WorkFlowActorUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorRole_RoleId",
                schema: "Project",
                table: "WorkFlowActorRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowActorGroup_GroupId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlow_MainAggregateId",
                schema: "Project",
                table: "WorkFlow",
                column: "MainAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlow_ManagerRoleID",
                schema: "Project",
                table: "WorkFlow",
                column: "ManagerRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_UserID",
                schema: "Project",
                table: "ProjectMember",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroup_GroupId",
                schema: "Project",
                table: "ProjectGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DomainID",
                schema: "Project",
                table: "Project",
                column: "DomainID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_PerformerUserId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "PerformerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "SourceStateId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_SourceStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "SourceStepId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "TargetStateId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueHistory_TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "TargetStepId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueDocument_DocumentId",
                schema: "IssueManagement",
                table: "IssueDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCustomFeild_IssueId",
                schema: "IssueManagement",
                table: "IssueCustomFeild",
                column: "IssueId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CurrenStepId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrenStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrentWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_MainAggregateId",
                schema: "IssueManagement",
                table: "Issue",
                column: "MainAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AttachStepId",
                schema: "DMS",
                table: "Documents",
                column: "AttachStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_MainAggregateId",
                schema: "DMS",
                table: "Documents",
                column: "MainAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchChiefOfficerId",
                schema: "Bank",
                table: "Branch",
                column: "BranchChiefOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchDeputyId",
                schema: "Bank",
                table: "Branch",
                column: "BranchDeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_LocationId",
                schema: "Bank",
                table: "Branch",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Location_LocationId",
                schema: "Bank",
                table: "Branch",
                column: "LocationId",
                principalSchema: "Basic",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Staff_BranchChiefOfficerId",
                schema: "Bank",
                table: "Branch",
                column: "BranchChiefOfficerId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Staff_BranchDeputyId",
                schema: "Bank",
                table: "Branch",
                column: "BranchDeputyId",
                principalSchema: "Organization",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_MainAggregate_MainAggregateId",
                schema: "DMS",
                table: "Documents",
                column: "MainAggregateId",
                principalSchema: "Authentication",
                principalTable: "MainAggregate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Step_AttachStepId",
                schema: "DMS",
                table: "Documents",
                column: "AttachStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "Issue",
                column: "MainAggregateId",
                principalSchema: "Authentication",
                principalTable: "MainAggregate",
                principalColumn: "Id");

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
                name: "FK_Issue_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrenStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "Issue",
                column: "CurrentWorkflowId",
                principalSchema: "Project",
                principalTable: "WorkFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueApproval_Step_WorkflowStepId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "WorkflowStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueApproval_WorkFlowActor_WorkflowActorId",
                schema: "IssueManagement",
                table: "IssueApproval",
                column: "WorkflowActorId",
                principalSchema: "Project",
                principalTable: "WorkFlowActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueCustomFeild_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueCustomFeild",
                column: "IssueId",
                principalSchema: "IssueManagement",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueDocument_Documents_DocumentId",
                schema: "IssueManagement",
                table: "IssueDocument",
                column: "DocumentId",
                principalSchema: "DMS",
                principalTable: "Documents",
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

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_State_TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "TargetStateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_Step_SourceStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "SourceStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_Step_TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "TargetStepId",
                principalSchema: "Project",
                principalTable: "Step",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueHistory_Users_PerformerUserId",
                schema: "IssueManagement",
                table: "IssueHistory",
                column: "PerformerUserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Domain_DomainID",
                schema: "Project",
                table: "Project",
                column: "DomainID",
                principalSchema: "Authentication",
                principalTable: "Domain",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroup_Groups_GroupId",
                schema: "Project",
                table: "ProjectGroup",
                column: "GroupId",
                principalSchema: "Authentication",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroup_Project_ProjectId",
                schema: "Project",
                table: "ProjectGroup",
                column: "ProjectId",
                principalSchema: "Project",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Users_UserID",
                schema: "Project",
                table: "ProjectMember",
                column: "UserID",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlow_MainAggregate_MainAggregateId",
                schema: "Project",
                table: "WorkFlow",
                column: "MainAggregateId",
                principalSchema: "Authentication",
                principalTable: "MainAggregate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlow_Role_ManagerRoleID",
                schema: "Project",
                table: "WorkFlow",
                column: "ManagerRoleID",
                principalSchema: "Authentication",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorGroup_Groups_GroupId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                column: "GroupId",
                principalSchema: "Authentication",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorGroup_WorkFlowActor_WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                column: "WorkFlowActorId",
                principalSchema: "Project",
                principalTable: "WorkFlowActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorRole_Role_RoleId",
                schema: "Project",
                table: "WorkFlowActorRole",
                column: "RoleId",
                principalSchema: "Authentication",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorUser_Users_UserId",
                schema: "Project",
                table: "WorkFlowActorUser",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowCompany_Company_CompanyId",
                schema: "Project",
                table: "WorkFlowCompany",
                column: "CompanyId",
                principalSchema: "Organization",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Location_LocationId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Staff_BranchChiefOfficerId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Staff_BranchDeputyId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_MainAggregate_MainAggregateId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Step_AttachStepId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_MainAggregate_MainAggregateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_State_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Step_CurrenStepId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueApproval_Step_WorkflowStepId",
                schema: "IssueManagement",
                table: "IssueApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueApproval_WorkFlowActor_WorkflowActorId",
                schema: "IssueManagement",
                table: "IssueApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueCustomFeild_Issue_IssueId",
                schema: "IssueManagement",
                table: "IssueCustomFeild");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueDocument_Documents_DocumentId",
                schema: "IssueManagement",
                table: "IssueDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_State_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_State_TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_Step_SourceStepId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_Step_TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueHistory_Users_PerformerUserId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Domain_DomainID",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGroup_Groups_GroupId",
                schema: "Project",
                table: "ProjectGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGroup_Project_ProjectId",
                schema: "Project",
                table: "ProjectGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_Users_UserID",
                schema: "Project",
                table: "ProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlow_MainAggregate_MainAggregateId",
                schema: "Project",
                table: "WorkFlow");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlow_Role_ManagerRoleID",
                schema: "Project",
                table: "WorkFlow");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorGroup_Groups_GroupId",
                schema: "Project",
                table: "WorkFlowActorGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorGroup_WorkFlowActor_WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorRole_Role_RoleId",
                schema: "Project",
                table: "WorkFlowActorRole");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowActorUser_Users_UserId",
                schema: "Project",
                table: "WorkFlowActorUser");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowCompany_Company_CompanyId",
                schema: "Project",
                table: "WorkFlowCompany");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowCompany_CompanyId",
                schema: "Project",
                table: "WorkFlowCompany");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActorUser_UserId",
                schema: "Project",
                table: "WorkFlowActorUser");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActorRole_RoleId",
                schema: "Project",
                table: "WorkFlowActorRole");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowActorGroup_GroupId",
                schema: "Project",
                table: "WorkFlowActorGroup");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlow_MainAggregateId",
                schema: "Project",
                table: "WorkFlow");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlow_ManagerRoleID",
                schema: "Project",
                table: "WorkFlow");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_UserID",
                schema: "Project",
                table: "ProjectMember");

            migrationBuilder.DropIndex(
                name: "IX_ProjectGroup_GroupId",
                schema: "Project",
                table: "ProjectGroup");

            migrationBuilder.DropIndex(
                name: "IX_Project_DomainID",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_IssueHistory_PerformerUserId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropIndex(
                name: "IX_IssueHistory_SourceStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropIndex(
                name: "IX_IssueHistory_SourceStepId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropIndex(
                name: "IX_IssueHistory_TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropIndex(
                name: "IX_IssueHistory_TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory");

            migrationBuilder.DropIndex(
                name: "IX_IssueDocument_DocumentId",
                schema: "IssueManagement",
                table: "IssueDocument");

            migrationBuilder.DropIndex(
                name: "IX_IssueCustomFeild_IssueId",
                schema: "IssueManagement",
                table: "IssueCustomFeild");

            migrationBuilder.DropIndex(
                name: "IX_IssueApproval_WorkflowActorId",
                schema: "IssueManagement",
                table: "IssueApproval");

            migrationBuilder.DropIndex(
                name: "IX_IssueApproval_WorkflowStepId",
                schema: "IssueManagement",
                table: "IssueApproval");

            migrationBuilder.DropIndex(
                name: "IX_Issue_CurrenStepId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_CurrentStateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_MainAggregateId",
                schema: "IssueManagement",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AttachStepId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_MainAggregateId",
                schema: "DMS",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Branch_BranchChiefOfficerId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_BranchDeputyId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_LocationId",
                schema: "Bank",
                table: "Branch");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorUser_WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorUser",
                newName: "IX_WorkFlowActorUser_WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorRole_WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorRole",
                newName: "IX_WorkFlowActorRole_WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "GroupID");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "ActiveStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_WorkFlowActorGroup_WorkFlowActorId",
                schema: "Project",
                table: "WorkFlowActorGroup",
                newName: "IX_WorkFlowActorGroup_WorkFlowActorID");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusId",
                schema: "Project",
                table: "WorkFlowActor",
                newName: "ActiveStatusID");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "Project",
                table: "ProjectGroup",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "Project",
                table: "ProjectGroup",
                newName: "GroupID");

            migrationBuilder.RenameColumn(
                name: "ActiveStatusId",
                schema: "Project",
                table: "ProjectGroup",
                newName: "ActiveStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectGroup_ProjectId",
                schema: "Project",
                table: "ProjectGroup",
                newName: "IX_ProjectGroup_ProjectID");

            migrationBuilder.AlterColumn<long>(
                name: "ManagerRoleID",
                schema: "Project",
                table: "WorkFlow",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TargetStepId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TargetStateId",
                schema: "IssueManagement",
                table: "IssueHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MainAggregateId",
                schema: "IssueManagement",
                table: "Issue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SourceId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MainAggregateId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AttachStepId",
                schema: "DMS",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroup_Project_ProjectID",
                schema: "Project",
                table: "ProjectGroup",
                column: "ProjectID",
                principalSchema: "Project",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowActorGroup_WorkFlowActor_WorkFlowActorID",
                schema: "Project",
                table: "WorkFlowActorGroup",
                column: "WorkFlowActorID",
                principalSchema: "Project",
                principalTable: "WorkFlowActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
