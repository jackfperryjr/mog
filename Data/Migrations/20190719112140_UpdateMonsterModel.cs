using Microsoft.EntityFrameworkCore.Migrations;

namespace Moogle.Data.Migrations
{
    public partial class UpdateMonsterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Monsters",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Monsters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Monsters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Monsters");
        }
    }
}
