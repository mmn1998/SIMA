using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMA.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateServiceTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputParam",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    InputName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputParam_DataType_DataTypeId",
                        column: x => x.DataTypeId,
                        principalSchema: "Basic",
                        principalTable: "DataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTask",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiMethodActionId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTask_ApiMethodAction_ApiMethodActionId",
                        column: x => x.ApiMethodActionId,
                        principalSchema: "Basic",
                        principalTable: "ApiMethodAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInputParam",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceTaskId = table.Column<long>(type: "bigint", nullable: false),
                    InputParamId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInputParam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceInputParam_InputParam_InputParamId",
                        column: x => x.InputParamId,
                        principalSchema: "Project",
                        principalTable: "InputParam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceInputParam_ServiceTask_ServiceTaskId",
                        column: x => x.ServiceTaskId,
                        principalSchema: "Project",
                        principalTable: "ServiceTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepServiceTask",
                schema: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ServiceTaskId = table.Column<long>(type: "bigint", nullable: false),
                    StepId = table.Column<long>(type: "bigint", nullable: false),
                    ActiveStatusID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepServiceTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepServiceTask_ServiceTask_ServiceTaskId",
                        column: x => x.ServiceTaskId,
                        principalSchema: "Project",
                        principalTable: "ServiceTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepServiceTask_Step_StepId",
                        column: x => x.StepId,
                        principalSchema: "Project",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputParam_DataTypeId",
                schema: "Project",
                table: "InputParam",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInputParam_InputParamId",
                schema: "Project",
                table: "ServiceInputParam",
                column: "InputParamId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInputParam_ServiceTaskId",
                schema: "Project",
                table: "ServiceInputParam",
                column: "ServiceTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTask_ApiMethodActionId",
                schema: "Project",
                table: "ServiceTask",
                column: "ApiMethodActionId");

            migrationBuilder.CreateIndex(
                name: "IX_StepServiceTask_ServiceTaskId",
                schema: "Project",
                table: "StepServiceTask",
                column: "ServiceTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StepServiceTask_StepId",
                schema: "Project",
                table: "StepServiceTask",
                column: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceInputParam",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "StepServiceTask",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "InputParam",
                schema: "Project");

            migrationBuilder.DropTable(
                name: "ServiceTask",
                schema: "Project");
        }
    }
}
