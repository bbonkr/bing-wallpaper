using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class AddTablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "AppLogs");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "AppLogs",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 28, 51, 348, DateTimeKind.Unspecified).AddTicks(9464), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 5, 23, 934, DateTimeKind.Unspecified).AddTicks(4295), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "작성시각");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppLogs",
                table: "AppLogs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppLogs",
                table: "AppLogs");

            migrationBuilder.RenameTable(
                name: "AppLogs",
                newName: "Logs");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "Logs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 5, 23, 934, DateTimeKind.Unspecified).AddTicks(4295), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 28, 51, 348, DateTimeKind.Unspecified).AddTicks(9464), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "작성시각");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");
        }
    }
}
