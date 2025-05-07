using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateApprovalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StepApprovalOptionId",
                schema: "IssueManagement",
                table: "IssueApproval",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApprovalOption",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StepApprovalOption",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    ApprovalOptionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepApprovalOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepApprovalOption_ApprovalOption_ApprovalOptionId",
                        column: x => x.ApprovalOptionId,
                        principalSchema: "Project",
                        principalTable: "ApprovalOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepApprovalOption_Step_StepId",
                        column: x => x.StepId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalOption_Code",
                schema: "Project",
                table: "ApprovalOption",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepApprovalOption_ApprovalOptionId",
                schema: "Project",
                table: "StepApprovalOption",
                column: "ApprovalOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StepApprovalOption_StepId",
                schema: "Project",
                table: "StepApprovalOption",
                column: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StepApprovalOption",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ApprovalOption",
                schema: "Project");

            migrationBuilder.DropColumn(
                name: "StepApprovalOptionId",
                schema: "IssueManagement",
                table: "IssueApproval");
        }
    }
}
