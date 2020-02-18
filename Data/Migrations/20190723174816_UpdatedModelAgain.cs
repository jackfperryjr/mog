using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class UpdatedModelAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weakness",
                table: "Monsters",
                newName: "JapaneseName");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Monsters",
                newName: "Game");

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

            migrationBuilder.AddColumn<string>(
                name: "ElementalWeakness",
                table: "Monsters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HitPoints",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManaPoints",
                table: "Monsters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ElementalWeakness",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "ManaPoints",
                table: "Monsters");

            migrationBuilder.RenameColumn(
                name: "JapaneseName",
                table: "Monsters",
                newName: "Weakness");

            migrationBuilder.RenameColumn(
                name: "Game",
                table: "Monsters",
                newName: "Strength");
        }
    }
}
