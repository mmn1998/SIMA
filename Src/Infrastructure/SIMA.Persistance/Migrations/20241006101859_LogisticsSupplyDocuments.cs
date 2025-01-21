using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class LogisticsSupplyDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogisticsSupplyDocument",
                schema: "Logistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LogisticsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticsSupplyDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticsSupplyDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "DMS",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LogisticsSupplyDocument_LogisticsSupply_LogisticsSupplyId",
                        column: x => x.LogisticsSupplyId,
                        principalSchema: "Logistics",
                        principalTable: "LogisticsSupply",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupplyDocument_DocumentId",
                schema: "Logistics",
                table: "LogisticsSupplyDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogisticsSupplyDocument_LogisticsSupplyId",
                schema: "Logistics",
                table: "LogisticsSupplyDocument",
                column: "LogisticsSupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticsSupplyDocument",
                schema: "Logistics");
        }
    }
}
