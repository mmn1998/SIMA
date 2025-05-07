using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceAvability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ServiceAvalibilityStartTime",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "TIME(7)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ServiceAvalibilityEndTime",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "TIME(7)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ServiceAvalibilityStartTime",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME(7)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ServiceAvalibilityEndTime",
                schema: "ServiceCatalog",
                table: "ServiceAvalibility",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME(7)");
        }
    }
}
