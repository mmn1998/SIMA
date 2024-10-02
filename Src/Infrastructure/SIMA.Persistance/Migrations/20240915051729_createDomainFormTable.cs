using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class createDomainFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomainForms",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FormId = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainForms_Domain_DomainId",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainForms_Form_FormId",
                        column: x => x.FormId,
                        principalSchema: "Authentication",
                        principalTable: "Form",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainForms_DomainId",
                schema: "Authentication",
                table: "DomainForms",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainForms_FormId",
                schema: "Authentication",
                table: "DomainForms",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainForms",
                schema: "Authentication");
        }
    }
}
