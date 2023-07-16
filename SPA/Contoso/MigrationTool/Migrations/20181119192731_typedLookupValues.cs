using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class typedLookupValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BooleanValue",
                table: "LookUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharValue",
                table: "LookUps",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeValue",
                table: "LookUps",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GuidValue",
                table: "LookUps",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpanValue",
                table: "LookUps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooleanValue",
                table: "LookUps");

            migrationBuilder.DropColumn(
                name: "CharValue",
                table: "LookUps");

            migrationBuilder.DropColumn(
                name: "DateTimeValue",
                table: "LookUps");

            migrationBuilder.DropColumn(
                name: "GuidValue",
                table: "LookUps");

            migrationBuilder.DropColumn(
                name: "TimeSpanValue",
                table: "LookUps");
        }
    }
}
