using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ForCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HasStoreProcedure",
                schema: "Project",
                table: "Progress",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataType",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsList = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    IsMultiSelect = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressStoreProcedure",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StoreProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressId = table.Column<long>(type: "bigint", nullable: false),
                    ExecutionOrdering = table.Column<float>(type: "real", nullable: false),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressStoreProcedure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressStoreProcedure_Progress_ProgressId",
                        column: x => x.ProgressId,
                        principalSchema: "Project",
                        principalTable: "Progress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StepOutputParam",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    DataTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsRequired = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepOutputParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepOutputParam_DataType_DataTypeId",
                        column: x => x.DataTypeId,
                        principalSchema: "Basic",
                        principalTable: "DataType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StepOutputParam_Step_StepId",
                        column: x => x.StepId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgressStoreProcedureParam",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProgressStoreProcedureId = table.Column<long>(type: "bigint", nullable: false),
                    DataTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRequired = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ActiveStatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressStoreProcedureParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressStoreProcedureParam_DataType_DataTypeId",
                        column: x => x.DataTypeId,
                        principalSchema: "Basic",
                        principalTable: "DataType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgressStoreProcedureParam_ProgressStoreProcedure_ProgressStoreProcedureId",
                        column: x => x.ProgressStoreProcedureId,
                        principalSchema: "Project",
                        principalTable: "ProgressStoreProcedure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataType_Code",
                schema: "Basic",
                table: "DataType",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedure_ProgressId",
                schema: "Project",
                table: "ProgressStoreProcedure",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedureParam_DataTypeId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStoreProcedureParam_ProgressStoreProcedureId",
                schema: "Project",
                table: "ProgressStoreProcedureParam",
                column: "ProgressStoreProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOutputParam_DataTypeId",
                schema: "Project",
                table: "StepOutputParam",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOutputParam_StepId",
                schema: "Project",
                table: "StepOutputParam",
                column: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressStoreProcedureParam",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "StepOutputParam",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ProgressStoreProcedure",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "DataType",
                schema: "Basic");

            migrationBuilder.DropColumn(
                name: "HasStoreProcedure",
                schema: "Project",
                table: "Progress");
        }
    }
}
