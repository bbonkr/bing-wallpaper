using Microsoft.EntityFrameworkCore.Migrations;

namespace Bing.Wallpaper.Data.SqlServer.Migrations
{
    public partial class Addimagewidthheight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Metadata_Height",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Metadata_Width",
                table: "Images",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata_Height",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Metadata_Width",
                table: "Images");
        }
    }
}
