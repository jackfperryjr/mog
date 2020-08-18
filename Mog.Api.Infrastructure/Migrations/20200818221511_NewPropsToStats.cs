using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Api.Infrastructure.Migrations
{
    public partial class NewPropsToStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Stats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Stats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Stats");
        }
    }
}
