using Microsoft.EntityFrameworkCore.Migrations;

namespace Moogle.Data.Migrations
{
    public partial class RenamedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weakness",
                table: "Monsters",
                newName: "ElementalWeakness");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Monsters",
                newName: "ElementalAffinity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElementalWeakness",
                table: "Monsters",
                newName: "Weakness");

            migrationBuilder.RenameColumn(
                name: "ElementalAffinity",
                table: "Monsters",
                newName: "Strength");
        }
    }
}
