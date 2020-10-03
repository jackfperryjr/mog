using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Api.Infrastructure.Migrations
{
    public partial class UpdateFeedModelAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateDeletion",
                table: "Feed");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "Feed",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StatDeletion",
                table: "Feed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserFirstName",
                table: "Feed",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "StatDeletion",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "UserFirstName",
                table: "Feed");

            migrationBuilder.AddColumn<int>(
                name: "StateDeletion",
                table: "Feed",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
