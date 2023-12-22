using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luciferin.Infrastructure.Storage.Migrations
{
    public partial class add_setting_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Settings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Settings");
        }
    }
}
