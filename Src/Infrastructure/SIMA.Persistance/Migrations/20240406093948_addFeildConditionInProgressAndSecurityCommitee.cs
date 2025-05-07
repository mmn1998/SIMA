using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addFeildConditionInProgressAndSecurityCommitee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SecurityCommitee");

            migrationBuilder.AddColumn<long>(
                name: "StateId",
                schema: "Project",
                table: "Step",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConditionExpression",
                schema: "Project",
                table: "Progress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApprovalResponsibleAnswerDocument",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalResponsibleAnswerDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalResponsibleAnswerDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalSupervisorAnswerDocument",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalSupervisorAnswerDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalSupervisorAnswerDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MeetingTurn = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meeting_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingHoldingReason",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingHoldingReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingHoldingStatus",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingHoldingStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsibleAnswerType",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleAnswerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivedBy = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Users_ArchivedBy",
                        column: x => x.ArchivedBy,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectPriority",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordering = table.Column<float>(type: "real", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorAnswerType",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorAnswerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsibleCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ResponsibleDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    ResponsibleStaffId = table.Column<long>(type: "bigint", nullable: true),
                    SupervisorCompanyId = table.Column<long>(type: "bigint", nullable: false),
                    SupervisorDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    SupervisorStaffId = table.Column<long>(type: "bigint", nullable: true),
                    IsPresented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSigned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivedBy = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approval_Company_ResponsibleCompanyId",
                        column: x => x.ResponsibleCompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Company_SupervisorCompanyId",
                        column: x => x.SupervisorCompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Department_ResponsibleDepartmentId",
                        column: x => x.ResponsibleDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Department_SupervisorDepartmentId",
                        column: x => x.SupervisorDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Meeting",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Staff_ResponsibleStaffId",
                        column: x => x.ResponsibleStaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Staff_SupervisorStaffId",
                        column: x => x.SupervisorStaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Approval_Users_ArchivedBy",
                        column: x => x.ArchivedBy,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingDocument",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingDocument_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingReason",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MeetingHoldingReasonId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingReason_MeetingHoldingReason_MeetingHoldingReasonId",
                        column: x => x.MeetingHoldingReasonId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "MeetingHoldingReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingReason_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingSchedule",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingHoldingStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingSchedule_MeetingHoldingStatus_MeetingHoldingStatusId",
                        column: x => x.MeetingHoldingStatusId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "MeetingHoldingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingSchedule_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectMeeting",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectPriorityId = table.Column<long>(type: "bigint", nullable: true),
                    IsOutOfOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmedBy = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectMeeting_Meeting_MeetingId",
                        column: x => x.MeetingId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Meeting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectMeeting_SubjectPriority_SubjectPriorityId",
                        column: x => x.SubjectPriorityId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "SubjectPriority",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectMeeting_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectMeeting_Users_ConfirmedBy",
                        column: x => x.ConfirmedBy,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovalResponsibleAnswer",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApprovalId = table.Column<long>(type: "bigint", nullable: false),
                    ResponsibleAnswerTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalResponsibleAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalResponsibleAnswer_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalResponsibleAnswer_ResponsibleAnswerType_ResponsibleAnswerTypeId",
                        column: x => x.ResponsibleAnswerTypeId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "ResponsibleAnswerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalSupervisorAnswer",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ApprovalId = table.Column<long>(type: "bigint", nullable: false),
                    SupervisorAnswerTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalSupervisorAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalSupervisorAnswer_Approval_ApprovalId",
                        column: x => x.ApprovalId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "Approval",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalSupervisorAnswer_SupervisorAnswerType_SupervisorAnswerTypeId",
                        column: x => x.SupervisorAnswerTypeId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "SupervisorAnswerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitees",
                schema: "SecurityCommitee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MeetingScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    StaffId = table.Column<long>(type: "bigint", nullable: true),
                    IsCompanyRepresentative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPresented = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSigned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitees_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Organization",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitees_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitees_MeetingSchedule_MeetingScheduleId",
                        column: x => x.MeetingScheduleId,
                        principalSchema: "SecurityCommitee",
                        principalTable: "MeetingSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitees_Staff_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Step_StateId",
                schema: "Project",
                table: "Step",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ArchivedBy",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "ArchivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_IssueId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_MeetingId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ResponsibleCompanyId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "ResponsibleCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ResponsibleDepartmentId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "ResponsibleDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_ResponsibleStaffId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "ResponsibleStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_SupervisorCompanyId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "SupervisorCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_SupervisorDepartmentId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "SupervisorDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Approval_SupervisorStaffId",
                schema: "SecurityCommitee",
                table: "Approval",
                column: "SupervisorStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalResponsibleAnswer_ApprovalId",
                schema: "SecurityCommitee",
                table: "ApprovalResponsibleAnswer",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalResponsibleAnswer_ResponsibleAnswerTypeId",
                schema: "SecurityCommitee",
                table: "ApprovalResponsibleAnswer",
                column: "ResponsibleAnswerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalResponsibleAnswerDocument_DocumentId",
                schema: "SecurityCommitee",
                table: "ApprovalResponsibleAnswerDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSupervisorAnswer_ApprovalId",
                schema: "SecurityCommitee",
                table: "ApprovalSupervisorAnswer",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSupervisorAnswer_SupervisorAnswerTypeId",
                schema: "SecurityCommitee",
                table: "ApprovalSupervisorAnswer",
                column: "SupervisorAnswerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalSupervisorAnswerDocument_DocumentId",
                schema: "SecurityCommitee",
                table: "ApprovalSupervisorAnswerDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_CompanyId",
                schema: "SecurityCommitee",
                table: "Invitees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_DepartmentId",
                schema: "SecurityCommitee",
                table: "Invitees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_MeetingScheduleId",
                schema: "SecurityCommitee",
                table: "Invitees",
                column: "MeetingScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitees_StaffId",
                schema: "SecurityCommitee",
                table: "Invitees",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Code",
                schema: "SecurityCommitee",
                table: "Meeting",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_IssueId",
                schema: "SecurityCommitee",
                table: "Meeting",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingDocument_DocumentId",
                schema: "SecurityCommitee",
                table: "MeetingDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingDocument_MeetingId",
                schema: "SecurityCommitee",
                table: "MeetingDocument",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingHoldingReason_Code",
                schema: "SecurityCommitee",
                table: "MeetingHoldingReason",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingHoldingStatus_Code",
                schema: "SecurityCommitee",
                table: "MeetingHoldingStatus",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReason_MeetingHoldingReasonId",
                schema: "SecurityCommitee",
                table: "MeetingReason",
                column: "MeetingHoldingReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReason_MeetingId",
                schema: "SecurityCommitee",
                table: "MeetingReason",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSchedule_MeetingHoldingStatusId",
                schema: "SecurityCommitee",
                table: "MeetingSchedule",
                column: "MeetingHoldingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSchedule_MeetingId",
                schema: "SecurityCommitee",
                table: "MeetingSchedule",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsibleAnswerType_Code",
                schema: "SecurityCommitee",
                table: "ResponsibleAnswerType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ArchivedBy",
                schema: "SecurityCommitee",
                table: "Subject",
                column: "ArchivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMeeting_ConfirmedBy",
                schema: "SecurityCommitee",
                table: "SubjectMeeting",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMeeting_MeetingId",
                schema: "SecurityCommitee",
                table: "SubjectMeeting",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMeeting_SubjectId",
                schema: "SecurityCommitee",
                table: "SubjectMeeting",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMeeting_SubjectPriorityId",
                schema: "SecurityCommitee",
                table: "SubjectMeeting",
                column: "SubjectPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorAnswerType_Code",
                schema: "SecurityCommitee",
                table: "SupervisorAnswerType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_State_StateId",
                schema: "Project",
                table: "Step",
                column: "StateId",
                principalSchema: "Project",
                principalTable: "State",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_State_StateId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropTable(
                name: "ApprovalResponsibleAnswer",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "ApprovalResponsibleAnswerDocument",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "ApprovalSupervisorAnswer",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "ApprovalSupervisorAnswerDocument",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "Invitees",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "MeetingDocument",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "MeetingReason",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "SubjectMeeting",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "ResponsibleAnswerType",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "Approval",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "SupervisorAnswerType",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "MeetingSchedule",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "MeetingHoldingReason",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "SubjectPriority",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "Subject",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "MeetingHoldingStatus",
                schema: "SecurityCommitee");

            migrationBuilder.DropTable(
                name: "Meeting",
                schema: "SecurityCommitee");

            migrationBuilder.DropIndex(
                name: "IX_Step_StateId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "StateId",
                schema: "Project",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "ConditionExpression",
                schema: "Project",
                table: "Progress");
        }
    }
}
