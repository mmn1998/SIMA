using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class trusty20250118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyShareFromWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MainShareFromWage",
                schema: "TrustyDraft",
                table: "TrustyDraft",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DraftOrderDate",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProformaAmount",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryRequest_DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "DraftOriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryRequest_DraftOrigin_DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest",
                column: "DraftOriginId",
                principalSchema: "TrustyDraft",
                principalTable: "DraftOrigin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "CurrentWorkflowId",
                principalSchema: "Project",
                principalTable: "WorkFlow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryRequest_DraftOrigin_DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory");

            migrationBuilder.DropIndex(
                name: "IX_InquiryRequest_DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "BuyShareFromWage",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "MainShareFromWage",
                schema: "TrustyDraft",
                table: "TrustyDraft");

            migrationBuilder.DropColumn(
                name: "DraftOrderDate",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "DraftOriginId",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.DropColumn(
                name: "ProformaAmount",
                schema: "TrustyDraft",
                table: "InquiryRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueChangeHistory_WorkFlow_CurrentWorkflowId",
                schema: "IssueManagement",
                table: "IssueChangeHistory",
                column: "CurrentWorkflowId",
                principalSchema: "Project",
                principalTable: "WorkFlow",
                principalColumn: "Id");
        }
    }
}
