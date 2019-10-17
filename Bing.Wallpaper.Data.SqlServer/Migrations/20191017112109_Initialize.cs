using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Hash = table.Column<string>(maxLength: 500, nullable: false),
                    Url = table.Column<string>(maxLength: 1000, nullable: false),
                    BaseUrl = table.Column<string>(maxLength: 1000, nullable: false),
                    FilePath = table.Column<string>(maxLength: 1000, nullable: false),
                    Directory = table.Column<string>(maxLength: 1000, nullable: false),
                    FileName = table.Column<string>(maxLength: 1000, nullable: false),
                    ContentType = table.Column<string>(maxLength: 30, nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
