using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Api.Infrastructure.Migrations
{
    public partial class NewFeedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharacterName = table.Column<string>(nullable: true),
                    Update = table.Column<int>(nullable: false),
                    Addition = table.Column<int>(nullable: false),
                    Deletion = table.Column<int>(nullable: false),
                    StatUpdate = table.Column<int>(nullable: false),
                    StatAddition = table.Column<int>(nullable: false),
                    StateDeletion = table.Column<int>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    UserPhoto = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feed");
        }
    }
}
