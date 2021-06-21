using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class AddImageMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metadata_Copyright",
                table: "Images",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "저작권");

            migrationBuilder.AddColumn<string>(
                name: "Metadata_CopyrightLink",
                table: "Images",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "저작권 링크");

            migrationBuilder.AddColumn<string>(
                name: "Metadata_Origin",
                table: "Images",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "원본 위치");

            migrationBuilder.AddColumn<string>(
                name: "Metadata_Title",
                table: "Images",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "제목");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "AppLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 5, 14, 4, 4, 40, 698, DateTimeKind.Unspecified).AddTicks(1962), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 28, 51, 348, DateTimeKind.Unspecified).AddTicks(9464), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "작성시각");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata_Copyright",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Metadata_CopyrightLink",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Metadata_Origin",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Metadata_Title",
                table: "Images");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Logged",
                table: "AppLogs",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 28, 51, 348, DateTimeKind.Unspecified).AddTicks(9464), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "작성시각",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 5, 14, 4, 4, 40, 698, DateTimeKind.Unspecified).AddTicks(1962), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "작성시각");
        }
    }
}
