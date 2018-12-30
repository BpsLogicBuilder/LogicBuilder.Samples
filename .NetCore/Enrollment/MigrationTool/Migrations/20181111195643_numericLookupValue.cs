using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class numericLookupValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LookUps",
                newName: "Text");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "LookUps",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<double>(
                name: "NumericValue",
                table: "LookUps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumericValue",
                table: "LookUps");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "LookUps",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "LookUps",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
