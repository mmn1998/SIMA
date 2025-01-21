using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserDomainAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDomainAccess",
                schema: "Authentication");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDomainAccess",
                schema: "Authentication",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DomainId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ActiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomainAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDomainAccess_Domain",
                        column: x => x.DomainId,
                        principalSchema: "Authentication",
                        principalTable: "Domain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDomainAccess_User",
                        column: x => x.UserId,
                        principalSchema: "Authentication",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDomainAccess_DomainId",
                schema: "Authentication",
                table: "UserDomainAccess",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDomainAccess_UserId",
                schema: "Authentication",
                table: "UserDomainAccess",
                column: "UserId");
        }
    }
}
