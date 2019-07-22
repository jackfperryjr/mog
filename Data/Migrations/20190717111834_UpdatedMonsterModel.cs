using Microsoft.EntityFrameworkCore.Migrations;

namespace Moogle.Data.Migrations
{
    public partial class UpdatedMonsterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ElementalAffinity",
                table: "Monsters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElementalWeakness",
                table: "Monsters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Game",
                table: "Monsters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hp",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mp",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Weakness",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "ElementalAffinity",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "ElementalWeakness",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Game",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Hp",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Mp",
                table: "Monsters");
        }
    }
}
