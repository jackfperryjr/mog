using Microsoft.EntityFrameworkCore.Migrations;

namespace Moogle.Data.Migrations
{
    public partial class RevertedMonster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
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
                name: "Hp",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "Mp",
                table: "Monsters");

            migrationBuilder.RenameColumn(
                name: "Game",
                table: "Monsters",
                newName: "Weakness");

            migrationBuilder.RenameColumn(
                name: "ElementalWeakness",
                table: "Monsters",
                newName: "Strength");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weakness",
                table: "Monsters",
                newName: "Game");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Monsters",
                newName: "ElementalWeakness");

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Monsters",
                nullable: true);

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
    }
}
