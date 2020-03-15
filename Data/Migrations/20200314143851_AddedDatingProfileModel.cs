using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class AddedDatingProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatingResponse_DatingProfile_DatingProfileId",
                table: "DatingResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatingResponse",
                table: "DatingResponse");

            migrationBuilder.RenameTable(
                name: "DatingResponse",
                newName: "Responses");

            migrationBuilder.RenameIndex(
                name: "IX_DatingResponse_DatingProfileId",
                table: "Responses",
                newName: "IX_Responses_DatingProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Responses",
                table: "Responses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_DatingProfile_DatingProfileId",
                table: "Responses",
                column: "DatingProfileId",
                principalTable: "DatingProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_DatingProfile_DatingProfileId",
                table: "Responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Responses",
                table: "Responses");

            migrationBuilder.RenameTable(
                name: "Responses",
                newName: "DatingResponse");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_DatingProfileId",
                table: "DatingResponse",
                newName: "IX_DatingResponse_DatingProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatingResponse",
                table: "DatingResponse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DatingResponse_DatingProfile_DatingProfileId",
                table: "DatingResponse",
                column: "DatingProfileId",
                principalTable: "DatingProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
