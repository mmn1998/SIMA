using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldForAccessFailed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessFaildDate",
                schema: "Authentication",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "Authentication",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedOverallCount",
                schema: "Authentication",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangePasswordDate",
                schema: "Authentication",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFaildDate",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessFailedOverallCount",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChangePasswordDate",
                schema: "Authentication",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "IsFirstLogin",
                schema: "Authentication",
                table: "Users",
                type: "char(1)",
                unicode: false,
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }
    }
}
