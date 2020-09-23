using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class AddLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false, comment: "식별자"),
                    MachineName = table.Column<string>(maxLength: 50, nullable: false, comment: "장치"),
                    Logged = table.Column<DateTimeOffset>(nullable: false, defaultValue: new DateTimeOffset(new DateTime(2020, 9, 21, 4, 5, 23, 934, DateTimeKind.Unspecified).AddTicks(4295), new TimeSpan(0, 0, 0, 0, 0)), comment: "작성시각"),
                    Level = table.Column<string>(maxLength: 50, nullable: false, comment: "로그 레벨"),
                    Message = table.Column<string>(nullable: false, comment: "메시지"),
                    Logger = table.Column<string>(nullable: true, comment: "로거"),
                    Callsite = table.Column<string>(nullable: true, comment: "사이트"),
                    Exception = table.Column<string>(nullable: true, comment: "예외")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                },
                comment: "로깅 NLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
