using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Api.Infrastructure.Migrations
{
    public partial class UpdateFeedReactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislike",
                table: "Feed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Love",
                table: "Feed",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "Love",
                table: "Feed");
        }
    }
}
