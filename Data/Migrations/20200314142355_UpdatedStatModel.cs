using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class UpdatedStatModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Character_PictureCollectionId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Character_CharacterId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_CharacterId",
                table: "Stats");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Stats",
                newName: "CollectionId");

            migrationBuilder.RenameColumn(
                name: "PictureCollectionId",
                table: "Pictures",
                newName: "CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_PictureCollectionId",
                table: "Pictures",
                newName: "IX_Pictures_CollectionId");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Stats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_CollectionId",
                table: "Stats",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Character_CollectionId",
                table: "Pictures",
                column: "CollectionId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Character_CollectionId",
                table: "Stats",
                column: "CollectionId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Character_CollectionId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Character_CollectionId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_CollectionId",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Stats");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Stats",
                newName: "CharacterId");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Pictures",
                newName: "PictureCollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_CollectionId",
                table: "Pictures",
                newName: "IX_Pictures_PictureCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_CharacterId",
                table: "Stats",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Character_PictureCollectionId",
                table: "Pictures",
                column: "PictureCollectionId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Character_CharacterId",
                table: "Stats",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
