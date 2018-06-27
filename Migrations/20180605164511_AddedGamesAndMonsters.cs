using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Moogle.Migrations
{
    public partial class AddedGamesAndMonsters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Picture = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    MonsterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    Strength = table.Column<string>(nullable: true),
                    Weakness = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.MonsterId);
                    table.ForeignKey(
                        name: "FK_Monsters_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_GameId",
                table: "Character",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_GameId",
                table: "Monsters",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Character_GameId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Character");
        }
    }
}
