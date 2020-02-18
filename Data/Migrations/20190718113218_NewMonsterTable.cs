using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class NewMonsterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Monsters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Monsters");
        }
    }
}
