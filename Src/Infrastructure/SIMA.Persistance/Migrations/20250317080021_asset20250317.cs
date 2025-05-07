using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class asset20250317 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationItemSupportTeam",
                schema: "AssetAndConfiguration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConfigurationItemId = table.Column<long>(type: "bigint", nullable: false),
                    MainStaffId = table.Column<long>(type: "bigint", nullable: false),
                    MainDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    MainBranchId = table.Column<long>(type: "bigint", nullable: true),
                    SubsitutedStaffId = table.Column<long>(type: "bigint", nullable: false),
                    SubsitutedDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    SubsitutedBranchId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItemSupportTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Branch_MainBranchId",
                        column: x => x.MainBranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Branch_SubsitutedBranchId",
                        column: x => x.SubsitutedBranchId,
                        principalSchema: "Bank",
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_ConfigurationItem_ConfigurationItemId",
                        column: x => x.ConfigurationItemId,
                        principalSchema: "AssetAndConfiguration",
                        principalTable: "ConfigurationItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Department_MainDepartmentId",
                        column: x => x.MainDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Department_SubsitutedDepartmentId",
                        column: x => x.SubsitutedDepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Staff_MainStaffId",
                        column: x => x.MainStaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigurationItemSupportTeam_Staff_SubsitutedStaffId",
                        column: x => x.SubsitutedStaffId,
                        principalSchema: "Organization",
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_ConfigurationItemId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "ConfigurationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_MainBranchId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "MainBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_MainDepartmentId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "MainDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_MainStaffId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "MainStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_SubsitutedBranchId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "SubsitutedBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_SubsitutedDepartmentId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "SubsitutedDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItemSupportTeam_SubsitutedStaffId",
                schema: "AssetAndConfiguration",
                table: "ConfigurationItemSupportTeam",
                column: "SubsitutedStaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItemSupportTeam",
                schema: "AssetAndConfiguration");
        }
    }
}
