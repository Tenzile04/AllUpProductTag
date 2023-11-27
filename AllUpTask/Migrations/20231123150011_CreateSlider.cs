using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllUpTask.Migrations
{
    public partial class CreateSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sliders");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sliders");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
