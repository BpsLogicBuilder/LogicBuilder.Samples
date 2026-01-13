using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationTool.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Instructor_InstructorID",
                table: "Department");

            migrationBuilder.DropTable(
                name: "RulesModule",
                schema: "Rules");

            migrationBuilder.DropTable(
                name: "VariableMetaData",
                schema: "Automatic");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Instructor_InstructorID",
                table: "Department",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Instructor_InstructorID",
                table: "Department");

            migrationBuilder.EnsureSchema(
                name: "Rules");

            migrationBuilder.EnsureSchema(
                name: "Automatic");

            migrationBuilder.CreateTable(
                name: "RulesModule",
                schema: "Rules",
                columns: table => new
                {
                    RulesModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Application = table.Column<string>(type: "varchar(100)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedInUserId = table.Column<string>(type: "varchar(256)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    ResourceSetFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RuleSetFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RulesModule", x => x.RulesModuleId);
                });

            migrationBuilder.CreateTable(
                name: "VariableMetaData",
                schema: "Automatic",
                columns: table => new
                {
                    VariableMetaDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableMetaData", x => x.VariableMetaDataId);
                });

            migrationBuilder.CreateIndex(
                name: "uc_RulesModule",
                schema: "Rules",
                table: "RulesModule",
                columns: new[] { "Name", "Application" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Instructor_InstructorID",
                table: "Department",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID");
        }
    }
}
