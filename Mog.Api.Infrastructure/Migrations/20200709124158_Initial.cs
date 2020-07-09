using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Api.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AddColumn<string>(
                name: "JapaneseName",
                table: "Characters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.DropColumn(
                name: "JapaneseName",
                table: "Characters");
        }
    }
}
