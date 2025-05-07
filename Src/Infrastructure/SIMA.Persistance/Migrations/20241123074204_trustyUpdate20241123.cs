using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trustyUpdate20241123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrustyDraftIssue",
                schema: "TrustyDraft");

            migrationBuilder.AddColumn<long>(
                name: "IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraft_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustyDraft_Issue_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                column: "IssueId",
                principalSchema: "IssueManagement",
                principalTable: "Issue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustyDraft_Issue_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropIndex(
                name: "IX_TrustyDraft_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.CreateTable(
                name: "TrustyDraftIssue",
                schema: "TrustyDraft",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DraftIssueTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IssueId = table.Column<long>(type: "bigint", nullable: false),
                    ReconsilationId = table.Column<long>(type: "bigint", nullable: true),
                    TrustyDraftId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustyDraftIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_DraftIssueType_DraftIssueTypeId",
                        column: x => x.DraftIssueTypeId,
                        principalSchema: "TrustyDraft",
                        principalTable: "DraftIssueType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_Issue_IssueId",
                        column: x => x.IssueId,
                        principalSchema: "IssueManagement",
                        principalTable: "Issue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_Reconsilation_ReconsilationId",
                        column: x => x.ReconsilationId,
                        principalSchema: "TrustyDraft",
                        principalTable: "Reconsilation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrustyDraftIssue_TrustyDraft_TrustyDraftId",
                        column: x => x.TrustyDraftId,
                        principalSchema: "TrustyDraft",
                        principalTable: "TrustyDraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_DraftIssueTypeId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "DraftIssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_IssueId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_ReconsilationId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "ReconsilationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrustyDraftIssue_TrustyDraftId",
                schema: "TrustyDraft",
                table: "TrustyDraftIssue",
                column: "TrustyDraftId");
        }
    }
}
