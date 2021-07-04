using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class FixCreateddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Images",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "sysdatetimeoffset()",
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUrl",
                table: "Images",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "기준 URL",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Images",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "newid()",
                comment: "식별자",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "AppLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "sysdatetimeoffset()",
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 5, 14, 4, 4, 40, 698, DateTimeKind.Unspecified).AddTicks(1962), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "작성시각");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AppLogs",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "newid()",
                comment: "식별자",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldComment: "식별자");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Images",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "sysdatetimeoffset()",
                oldComment: "작성시각");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUrl",
                table: "Images",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "기준 URL");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Images",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "newid()",
                oldComment: "식별자");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "AppLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 5, 14, 4, 4, 40, 698, DateTimeKind.Unspecified).AddTicks(1962), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "sysdatetimeoffset()",
                oldComment: "작성시각");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AppLogs",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                comment: "식별자",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "newid()",
                oldComment: "식별자");
        }
    }
}
